﻿using System;
using System.Collections.Generic;
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

        [HttpGet]
        [JsonErrorHandler]
        public ActionResult GetIncomingChallenges()
        {
            var challenges =
                    UnitOfWork.All<Challenge>(c => c.ChallengingPlayer.Id != CurrentUser.Id && c.Accepted == null)
                        .Where(c => c.LightPlayer.Id == CurrentUser.Id || c.DarkPlayer.Id == CurrentUser.Id);

            var openChallenges = challenges.Select(c => new ExistingChallengeViewModel()
            {
                Accepted = false,
                ChallengeTitle = c.Title,
                DateTime = c.DateTime,
                Id = c.ChallengeId,
                Opponent = c.ChallengingPlayer,
                OpponentTeam = c.LightPlayer == c.ChallengingPlayer ? Team.Light : Team.Dark
            }).ToList();

            return Json(openChallenges, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [JsonErrorHandler]
        public ActionResult GetActiveGames()
        {
            var lightGames = UnitOfWork.All<Game>(g => g.LightPlayer == CurrentUser);
            var darkGames = UnitOfWork.All<Game>(g => g.DarkPlayer == CurrentUser);

            var gameModels = new List<GamePreviewViewModel>();
            foreach (var game in lightGames)
            {
                var model = AutoMapper.Mapper.Map<GamePreviewViewModel>(game);
                model.IsPlayersTurn = game.MoveCount % 2 == 0;
                model.OpponentName = game.DarkPlayer.DisplayName;
                gameModels.Add(model);
            }
            foreach (var game in darkGames)
            {
                var model = AutoMapper.Mapper.Map<GamePreviewViewModel>(game);
                model.IsPlayersTurn = game.MoveCount % 2 == 1;
                model.OpponentName = game.LightPlayer.DisplayName;
                gameModels.Add(model);
            }

            return Json(gameModels.OrderBy(g => !g.IsPlayersTurn), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [JsonErrorHandler]
        public ActionResult MakeMove(long id, Move move)
        {
            var game = GetChessGame(id);
            var gameManager = new GameManager(game);

            if (!IsPlayersMove(game, CurrentUser))
                throw new Exception("It is not your turn!");

            try
            {
                var movingTeam = gameManager.TeamToMove();

                gameManager.MovePiece(move);

                if (gameManager.IsDraw())
                    gameManager.MarkGameAsDraw();
                else if (gameManager.IsCheckmate())
                    gameManager.MarkWinningTeam(movingTeam);

                UnitOfWork.Commit();

                var model = GetGameModel(game);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw NewMoveFailureException(move, ex);
            }
        }

        private Exception NewMoveFailureException(Move move, Exception ex)
        {
            var start = move.StartColumn + ", " + move.StartRow;
            var end = move.EndColumn + ", " + move.EndRow;

            var reasonForFailure = ex.Message;

            return new ArgumentException(String.Format("Moving {0} to {1} is illegal. {2}",
                start, end, reasonForFailure));
        }

        [HttpGet]
        [JsonErrorHandler]
        public ActionResult GetGame(long id)
        {
            var game = GetChessGame(id);

            if (game == null)
                throw new ArgumentException(String.Format("Game {0} does not exist.", id));

            var model = GetGameModel(game);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private bool IsPlayersMove(Game game, ChessUser player)
        {
            var playerId = CurrentUser.Id;
            if (playerId == game.LightPlayer.Id && game.MoveCount % 2 == 0)
                return true;
            if (playerId == game.DarkPlayer.Id && game.MoveCount % 2 == 1)
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
            var winnerPlayer = (PlayerViewModel)null;

            if (game.WinnerPlayer != null)
                winnerPlayer = AutoMapper.Mapper.Map<PlayerViewModel>(game.WinnerPlayer);

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
                IsCurrentPlayersMove = IsPlayersMove(game, CurrentUser),
                Complete = game.Complete,
                Winner = winnerPlayer,
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

            var pieceCaster = new PieceCaster();

            foreach (var square in game.Squares.Where(s => s.ChessPiece != null))
            {
                square.ChessPiece = pieceCaster.MapPiece(square.ChessPiece);
            }

            return game;
        }
    }
}
