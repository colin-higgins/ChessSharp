using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessSharp.Models;

namespace ChessSharp.Controllers
{
    public class HomeController : Controller
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

        public ActionResult PlayChess()
        {
            //This is the initial state of a chessboard as per the Piece enum
            SharpCentral.Piece[] chessBoard = (new SharpCentral.FreshGame()).chessBoard;

            var model = new TempGameModel()
            {
                board = chessBoard,
                playerLightIdent = 1,
                playerDarkIdent = 2,
                lightScore = 0,
                darkScore = 0,
                moveHistory = ""
            };

            return View(model);
        }
    }
}
