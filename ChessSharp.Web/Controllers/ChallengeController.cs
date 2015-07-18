using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Data.Enum;
using Chess.Domain;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    [Authorize]
    public class ChallengeController : BaseController
    {
        [HttpGet]
        public ActionResult Make()
        {
            var playersToChallenge = UnitOfWork.All<ChessUser>(p => p.Id != CurrentUser.Id);

            var currentChallenges =
                UnitOfWork.All<Challenge>(c => c.ChallengingPlayer.Id == CurrentUser.Id);

            var model = new CreateChallengeViewModel
            {
                Players = playersToChallenge.Select(AutoMapper.Mapper.Map<PlayerViewModel>).ToList(),
                CurrentChallenges = currentChallenges.Select(c => new ChallengeViewModel
                {
                    Accepted = c.Accepted.HasValue && c.Accepted.Value,
                    ChallengerId = c.ChallengingPlayer.Id,
                    ChallengeTitle = c.Title,
                    DarkPlayerId = c.DarkPlayer.Id,
                    LightPlayerId = c.LightPlayer.Id

                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Make(CreateChallengeViewModel model)
        {
            model.ChallengerId = CurrentUser.Id;

            var opponent = UnitOfWork.Find<ChessUser>(model.OpponentId);

            var challenge = new Challenge()
            {
                ChallengingPlayer = CurrentUser,
                DarkPlayer = model.IsPlayerDark ? CurrentUser : opponent,
                LightPlayer = !model.IsPlayerDark ? CurrentUser : opponent,
                DateTime = DateTime.Now,
                Title = model.ChallengeTitle
            };

            UnitOfWork.Add(challenge);
            UnitOfWork.Commit();

            return RedirectToAction("Make");
        }

        [HttpGet]
        public ActionResult Decide(long id)
        {
            var challenge = UnitOfWork.Find<Challenge>(id);

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

            var challenge = UnitOfWork.Find<Challenge>(model.Id);

            if (challenge.Accepted == true)
                return RedirectToAction("Accepted", new { model = challenge });
            if (challenge.Accepted == false)
                return RedirectToAction("Declined", new { model = challenge });

            return RedirectToAction("Decide", new {id = model.Id});
        }

        public ActionResult Accepted(Challenge model)
        {
            return View();
        }

        public ActionResult Declined(Challenge model)
        {
            return View();
        }

        private void RespondToChallenge(long challengeId, bool accepted)
        {
            var challenge = UnitOfWork.Find<Challenge>(challengeId);

            if (accepted)
            {
                var board = new Board();
                var squares = board.Squares.SelectMany(s => s);

                var game = new Game
                {
                    DarkPlayer = challenge.DarkPlayer,
                    LightPlayer = challenge.LightPlayer,
                    Name = challenge.Title,
                    Challenge = challenge,
                };

                foreach (var square in squares)
                {
                    square.Game = game;
                    square.ChessPiece = (ChessPiece) square.ChessPiece; //TODO: Why did I do this?
                    UnitOfWork.Add(square);
                }

                UnitOfWork.Add(game);
            }

            challenge.Accepted = accepted;
            UnitOfWork.Commit();
        }
	}
}