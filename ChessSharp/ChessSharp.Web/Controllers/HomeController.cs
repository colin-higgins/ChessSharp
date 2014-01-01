using System;
using System.Linq;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using ChessSharp.Web.App_Start;
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
        [HttpGet]
        public ActionResult Challenge()
        {
            var username = HttpContext.User.Identity.Name;
            var currentChessUser = _unitOfWork
                                    .All<ChessUser>(u => String.Equals(u.UserName, username, StringComparison.CurrentCultureIgnoreCase))
                                    .FirstOrDefault();
            var playersToChallenge = _unitOfWork.All<Player>(p => p.ChessUserId != currentChessUser.ChessUserId);
            var currentChallenges =
                _unitOfWork.All<Challenge>(c => c.ChallengingPlayerId == currentChessUser.ChessUserId);
            var model = new CreateChallengeViewModel
            {
                Players = playersToChallenge.Select(p => new PlayerViewModel()
                {
                    Name = p.DisplayName,
                    PlayerId = p.PlayerId
                }).ToList(),
                CurrentChallenges = currentChallenges.Select(c => new ChallengeViewModel
                {
                    Accepted = c.Accepted,
                    ChallengerId = c.ChallengingPlayerId,
                    ChallengeTitle = c.Title,
                    DarkPlayerId = c.DarkPlayerId,
                    LightPlayerId = c.LightPlayerId

                }).ToList()
            };

            return View(model);
        }

        public ActionResult Challenge(CreateChallengeViewModel model)
        {
            return RedirectToAction("Challenge");
        }
    }
}