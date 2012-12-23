using System.Web.Mvc;

namespace ChessSharp.Controllers
{
    public class HelloWorldController : Controller
    {
        public string Index()
        {
            return "This is my default DERP...";
        }

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}