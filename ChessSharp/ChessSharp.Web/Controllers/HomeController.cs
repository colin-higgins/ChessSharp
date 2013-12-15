using System.Web.Mvc;

namespace ChessSharp.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Play()
        {
            return View();
        }

        [Authorize]
        public ActionResult Challenge()
        {
            return View();
        }
    }
}