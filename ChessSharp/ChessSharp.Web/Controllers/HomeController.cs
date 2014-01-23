using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chess.Data.Entities;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Play()
        {
            if (CurrentPlayer == null)
                return RedirectToAction("RegisterPlayer");

            var games = GetGamesForPlayer();

            var gamesViewModel = games.Select(g => new ActiveGameViewModel()
            {
                GameId = g.GameId,
                CurrentPlayerId = CurrentPlayer.PlayerId,
                DarkPlayerName = g.DarkPlayer.DisplayName,
                LightPlayerName = CurrentPlayer.DisplayName,
                Name = g.Name
            });

            return View(gamesViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RegisterPlayer()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult RegisterPlayer(PlayerViewModel model)
        {
            return RedirectToAction("Play");
        }

        private IEnumerable<Game> GetGamesForPlayer()
        {
            var id = CurrentPlayer.PlayerId;

            var currentGames = _unitOfWork.All<Game>(g => g.DarkPlayerId == id || g.LightPlayerId == id);

            return currentGames.ToList();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Challenge()
        {
            if (CurrentPlayer == null)
                throw new Exception("This username does not have an associated CurrentPlayer. Please create one in the CurrentPlayer registration screen.");

            var playersToChallenge = _unitOfWork.All<Player>(p => p.PlayerId != CurrentPlayer.PlayerId);

            var currentChallenges =
                _unitOfWork.All<Challenge>(c => c.ChallengingPlayerId == CurrentPlayer.PlayerId);

            var model = new CreateChallengeViewModel
            {
                Players = playersToChallenge.Select(p => new PlayerViewModel()
                {
                    Name = p.DisplayName,
                    PlayerId = p.PlayerId
                }).ToList(),
                CurrentChallenges = currentChallenges.Select(c => new ChallengeViewModel
                {
                    Accepted = c.Accepted.HasValue ? c.Accepted.Value : false,
                    ChallengerId = c.ChallengingPlayerId,
                    ChallengeTitle = c.Title,
                    DarkPlayerId = c.DarkPlayerId,
                    LightPlayerId = c.LightPlayerId

                }).ToList()
            };

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Challenge(CreateChallengeViewModel model)
        {
            var challenge = new Challenge()
            {
                ChallengingPlayer = CurrentPlayer,
                DarkPlayerId = model.IsPlayerDark ? CurrentPlayer.PlayerId : model.OpponentId,
                LightPlayerId = !model.IsPlayerDark ? CurrentPlayer.PlayerId : model.OpponentId,
                DateTime = DateTime.Now,
                Title = model.ChallengeTitle
            };

            _unitOfWork.Add(challenge);
            _unitOfWork.Commit();

            return RedirectToAction("Challenge");
        }
    }
}