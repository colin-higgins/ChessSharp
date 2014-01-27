using System;
using System.Linq;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class ChessApiController : BaseController
    {
        public ActionResult Index()
        {
            throw new NotImplementedException("This method has not been implemented.");
        }

        public ActionResult MakeMove(long id, Move move)
        {
            var game = GetChessGame(id);

            if (game == null)
                throw new ArgumentException(String.Format("Game {0} does not exist.", id));

            var model = GetGameModel(game);

            var success = model.MovePiece(move);

            if (success)
                return Json(model, JsonRequestBehavior.AllowGet);

            var start = move.StartColumn + ", " + move.StartRow;
            var end = move.EndColumn + ", " + move.EndRow;

            throw new ArgumentException(String.Format("Moving {0} to {1} is illegal for game {2}",
                start, end, id));
        }

        public ActionResult GetGame(long id)
        {
            var game = GetChessGame(id);

            if (game == null)
                throw new ArgumentException(String.Format("Game {0} does not exist.", id));

            var model = GetGameModel(game);

            return Json(game, JsonRequestBehavior.AllowGet);
        }

        public GameModel GetGameModel(Game game)
        {
            var squares = game.Squares.ToArray();

            if (!squares.Any())
            {
                var missingSquares = new Board().Squares.SelectMany(s => s).ToArray();

                foreach (var square in missingSquares)
                {
                    square.Game = game;
                    square.ChessPiece = (ChessPiece)square.ChessPiece;
                    UnitOfWork.Add(square);
                }

                UnitOfWork.Commit();
                squares = missingSquares;
            }

            var moves = game.Moves.ToList();
            var board = new Board(squares);

            var gameModel = new GameModel()
            {
                Board = board,
                DarkScore = game.DarkScore,
                LightScore = game.LightScore,
                GameId = game.GameId,
                MoveCount = game.MoveCount,
                PlayerDark = game.DarkPlayer,
                PlayerLight = game.LightPlayer,
                Moves = moves
            };

            return gameModel;
        }

        private Game GetChessGame(long id)
        {
            var game = UnitOfWork.Find<Game>(id);

            return game;
        }
    }
}
