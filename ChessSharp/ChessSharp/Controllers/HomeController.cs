using System.Web.Mvc;
using Chess.Data;

namespace ChessSharp.Controllers
{
    public class HomeController : ApplicationController
    {
        private IUnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new ChessContext();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
