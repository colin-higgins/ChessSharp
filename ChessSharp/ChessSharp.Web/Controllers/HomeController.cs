using System.Linq;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using ChessSharp.Web.Models;
using Microsoft.AspNet.Identity;

namespace ChessSharp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new ChessContext();
        }

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
            var currentUser = HttpContext.User;
            var username = currentUser.Identity.Name;
            var playersToChallenge = _unitOfWork.All<Player>(p => p.ChessUser.UserName != username);

            var model = new ChallengeViewModel
            {
                Players = playersToChallenge.Select(p => new PlayerViewModel()
                {
                    Name = p.DisplayName,
                    PlayerId = p.PlayerId
                }).ToList(),
            };

            return View(model);
        }
    }
}