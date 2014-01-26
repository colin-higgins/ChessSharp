using System;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using ChessSharp.Models;

namespace ChessSharp.Controllers
{
    public class ChessApiController : ApplicationController
    {
        public ActionResult Index()
        {
            throw new NotImplementedException("This method has not been implemented.");
        }

        public ActionResult MakeMove(long id, Move move)
        {
            var game = GetChessGame(id);

            var success = game.MovePiece(move);

            if (success)
                return Json(game, JsonRequestBehavior.AllowGet);

            var start = move.StartColumn + ", " + move.StartRow;
            var end = move.EndColumn + ", " + move.EndRow;

            throw new ArgumentException(String.Format("Moving {0} to {1} is illegal for game {2}",
                start, end, id));
        }

        public ActionResult GetGame(long? id)
        {
            var game = GetChessGame(id);

            if (id.HasValue && game == null)
                throw new ArgumentException(String.Format("Game {0} does not exist.", id));

            return Json(game, JsonRequestBehavior.AllowGet);
        }

        private GameModel GetChessGame(long? id)
        {
            var game = (GameModel)Session["model"];

            if (game == null)
            {
                game = new GameModel() { GameId = id ?? 0 };
                Session["model"] = game;
            }
            else if (id.HasValue && game.GameId != id)
            {
                game = new GameModel() { GameId = id.Value };
                Session["model"] = game;
            }

            return game;

        }
    }
}
