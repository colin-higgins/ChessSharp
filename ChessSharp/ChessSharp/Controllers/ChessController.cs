using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessSharp.Models;

using SharpCentral;

namespace ChessSharp.Controllers
{
    public class ChessController : Controller
    {
        ChessSharpEntities db = new ChessSharpEntities();
        SharpCentral.FreshGame chessProp = new SharpCentral.FreshGame();
        GameModel model { get; set; }

        public ActionResult PlayChess()
        {
            //This is the initial state of a chessboard as per the Piece enum
            SharpCentral.Piece[] chessBoard = chessProp.chessBoard;

            model = new GameModel(1, 2);

            return View(model);
        }

        [HttpPost]
        public ActionResult PlayGame(FormCollection collection)
        {



            return View("PlayChess", model);
        }
    }
}