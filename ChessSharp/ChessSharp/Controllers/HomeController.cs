using System;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using ChessSharp.Models;

namespace ChessSharp.Controllers
{
    public class HomeController : ApplicationController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PlayChess(long? gameId = null)
        {
            var model = GetGame(gameId ?? 0);

            return View(model);
        }

        public Square[][] MakeMove(long gameId, Move move)
        {
            var game = GetGame(gameId);

            var success = game.MovePiece(move);

            if (success)
                return game.Board.Squares;

            var start = move.StartColumn + ", " + move.StartRow;
            var end = move.EndColumn + ", " + move.EndRow;

            AddUserError(String.Format("Moving {0} to {1} is illegal for game {2}",
                start, end, gameId));

            return game.Board.Squares;
        }

        public GameModel GetGame(long gameId)
        {
            var game = (GameModel)Session["model"];

            if (game == null)
            {
                game = new GameModel();
                Session["model"] = game;
            }

            return game;
        }
    }
}
