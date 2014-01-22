using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Data.Enum;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChallengeController()
        {
            _unitOfWork = new ChessContext();
        }

        [HttpGet]
        public ActionResult Decide(long id)
        {
            var challenge = _unitOfWork.Find<Challenge>(id);

            if (challenge.Accepted == true)
                return RedirectToAction("Accepted", new { model = challenge });
            if (challenge.Accepted == false)
                return RedirectToAction("Declined", new { model = challenge });

            var opponentTeam = Team.Dark;

            if (challenge.LightPlayer == challenge.ChallengingPlayer)
                opponentTeam = Team.Light;

            var model = new ExistingChallengeViewModel()
            {
                Accepted = false,
                ChallengeTitle = challenge.Title,
                DateTime = challenge.DateTime,
                Id = challenge.ChallengeId,
                Opponent = challenge.ChallengingPlayer,
                OpponentTeam = opponentTeam
            };

            return View(model);
        }


        [HttpPost]
        public ActionResult Decide(ExistingChallengeViewModel model)
        {
            RespondToChallenge(model.Id, model.Accepted);

            var challenge = _unitOfWork.Find<Challenge>(model.Id);

            if (challenge.Accepted == true)
                return RedirectToAction("Accepted", new { model = challenge });
            if (challenge.Accepted == false)
                return RedirectToAction("Declined", new { model = challenge });
        }

        public ActionResult Accepted(Challenge model)
        {
            return View(model);
        }

        public ActionResult Declined(Challenge model)
        {
            return View(model);
        }

        private void RespondToChallenge(long challengeId, bool accepted)
        {
            
        }
	}
}