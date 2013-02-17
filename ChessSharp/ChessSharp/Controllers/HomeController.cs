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

        ChessSharpEntities db = new ChessSharpEntities();
        SharpCentral.FreshGame chessProp = new SharpCentral.FreshGame();
        GameModel model { get; set; }

        public ActionResult PlayChess(FormCollection collection)
        {
            bool success = false;
            int currentPosition, newPosition;

            model = (GameModel)Session["model"];

            if (model == null)
            {
                //This is the initial state of a chessboard as per the Piece enum
                SharpCentral.Piece[] chessBoard = chessProp.chessBoard;
                model = new GameModel(1, 2);
                Session["model"] = model;
            }

            if (collection != null)
            {
                if (collection.Get("currentPosition") != null && collection.Get("newPosition") != null)
                {
                    int.TryParse(collection.Get("currentPosition").Replace("sq", ""), out currentPosition);
                    int.TryParse(collection.Get("newPosition").Replace("sq", ""), out newPosition);

                    success = model.MovePiece(currentPosition, newPosition);

                    if (!success)
                    {
                        ViewBag.MoveFailed = "That was an illegal move! ";
                    }
                    else
                    {
                        //Save the move and return the model
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult PlayGame(FormCollection collection)
        {

            bool success = false;
            int currentPosition, newPosition;

            model = (GameModel)Session["model"];

            if (model == null)
            {
                //This is the initial state of a chessboard as per the Piece enum
                SharpCentral.Piece[] chessBoard = chessProp.chessBoard;
                model = new GameModel(1, 2);
            }

            if (collection != null)
            {
                if (collection.Get("currentPosition") != null && collection.Get("newPosition") != null)
                {
                    int.TryParse(collection.Get("currentPosition").Replace("sq", ""), out currentPosition);
                    int.TryParse(collection.Get("newPosition").Replace("sq", ""), out newPosition);

                    success = model.MovePiece(currentPosition, newPosition);

                    if (!success)
                    {
                        ViewBag.MoveFailed = "That was an illegal move! ";
                    }
                    else
                    {
                        //Save the move and return the model
                    }
                }
            }
            return View("PlayChess", model);
        }
    }
}
