using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessSharp.Models;

using sharpCentral;

namespace ChessSharp.Controllers
{
    public class ChessController : Controller
    {
        ChessSharpEntities db = new ChessSharpEntities();
        sharpCentral.chessGameProperties chessProp = new sharpCentral.chessGameProperties();

        TempGameModel model { get; set; }

        public ActionResult Index()
        {
            //This is the initial state of a chessboard as per the Piece enum
            sharpCentral.pieceType[] chessBoard = chessProp.chessBoard;

            model = new TempGameModel()
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

        //[HttpPost, ActionName("Index")]
        public JsonResult MakeMove(int startIndex, int moveToIndex)
        {
            bool validMove = false;

            return new JsonResult { Data = new { valid = validMove } };
        }
    }
}