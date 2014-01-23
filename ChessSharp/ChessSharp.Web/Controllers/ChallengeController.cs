using System.Web.Mvc;
using Chess.Data.Entities;
using Chess.Data.Enum;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class ChallengeController : BaseController
    {
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

            return RedirectToAction("Decide", new {id = model.Id});
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
            var challenge = _unitOfWork.Find<Challenge>(challengeId);
            if (accepted)
            {
                var game = new Game
                {
                    DarkPlayer = challenge.DarkPlayer,
                    LightPlayer = challenge.LightPlayer,
                    Name = challenge.Title
                };
                _unitOfWork.Add(game);
            }
            challenge.Accepted = accepted;
            _unitOfWork.Commit();
        }
	}
}