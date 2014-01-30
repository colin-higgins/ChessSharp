using System;
using System.Linq;
using System.Web.Mvc;
using Chess.Data.Entities;
using Chess.Data.Enum;
using Chess.Domain;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class ChessApiController : BaseController
    {
        public class JsonErrorHandlerAttribute : FilterAttribute, IExceptionFilter
        {
            public void OnException(ExceptionContext filterContext)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        error =
                            filterContext.Exception.ToString(),
                        message = filterContext.Exception.Message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [JsonErrorHandler]
        public ActionResult Index()
        {
            throw new NotImplementedException("This method has not been implemented.");
        }

        [JsonErrorHandler]
        public ActionResult GetActiveGames()
        {
            var games = UnitOfWork.All<Game>(g => g.DarkPlayer == CurrentPlayer || g.LightPlayer == CurrentPlayer);

            var gameModels = games.Select(AutoMapper.Mapper.Map<ActiveGameViewModel>);

            return Json(gameModels, JsonRequestBehavior.AllowGet);
        }

        [JsonErrorHandler]
        public ActionResult MakeMove(long id, Move move)
        {
            var game = GetChessGame(id);
            var gameManager = new GameManager(game);

            if (!IsPlayersMove(game, CurrentPlayer))
                throw new Exception("It is not your turn!");

            try
            {
                gameManager.MovePiece(move);
                UnitOfWork.Commit();

                var model = GetGameModel(game);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var start = move.StartColumn + ", " + move.StartRow;
                var end = move.EndColumn + ", " + move.EndRow;

                var reasonForFailure = ex.Message;

                throw new ArgumentException(String.Format("Moving {0} to {1} is illegal. {2}",
                    start, end, reasonForFailure));
            }
        }

        [JsonErrorHandler]
        public ActionResult GetGame(long id)
        {
            var game = GetChessGame(id);

            if (game == null)
                throw new ArgumentException(String.Format("Game {0} does not exist.", id));

            var model = GetGameModel(game);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private bool IsPlayersMove(Game game, Player player)
        {
            var playerId = CurrentPlayer.PlayerId;
            if (playerId == game.LightPlayer.PlayerId && game.MoveCount % 2 == 0)
                return true;
            if (playerId == game.DarkPlayer.PlayerId && game.MoveCount % 2 == 1)
                return true;
            return false;
        }

        private GameModel GetGameModel(Game game)
        {
            var squares = game.Squares.ToArray();

            squares = RepairMissingSquares(game, squares);

            var moves = game.Moves.ToList();
            var board = new Board(squares);

            var boardModel = AutoMapper.Mapper.Map<BoardViewModel>(board);
            var darkPlayerModel = AutoMapper.Mapper.Map<PlayerViewModel>(game.DarkPlayer);
            var lightPlayerModel = AutoMapper.Mapper.Map<PlayerViewModel>(game.LightPlayer);

            var gameModel = new GameModel()
            {
                Board = boardModel,
                Name = game.Name,
                DarkScore = game.DarkScore,
                LightScore = game.LightScore,
                Id = game.Id,
                MoveCount = game.MoveCount,
                PlayerDark = darkPlayerModel,
                PlayerLight = lightPlayerModel,
                Moves = moves,
                IsCurrentPlayersMove = IsPlayersMove(game, CurrentPlayer)
            };

            return gameModel;
        }

        private Square[] RepairMissingSquares(Game game, Square[] squares)
        {
            if (!squares.Any())
            {
                var missingSquares = new Board().Squares.SelectMany(s => s).ToArray();

                foreach (var square in missingSquares)
                {
                    square.Game = game;
                    square.ChessPiece = square.ChessPiece;
                    UnitOfWork.Add(square);
                }

                UnitOfWork.Commit();
                squares = missingSquares;
            }
            return squares;
        }

        private Game GetChessGame(long id)
        {
            var game = UnitOfWork.Find<Game>(id);
                                
            if (game == null)
                throw new ArgumentException(String.Format("Game {0} does not exist.", id));

            return game;
        }
    }
}
