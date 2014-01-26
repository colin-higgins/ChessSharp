﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Chess.Data.Entities;
using Chess.Domain.Repositories;
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
                return RedirectToAction("RegisterPlayer", new { actionName = "Play" });

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
        public ActionResult RegisterPlayer(string actionName)
        {
            var repository = new PlayerRepository(_unitOfWork);
            repository.AddFromUser(CurrentUser);
            _unitOfWork.Commit();

            return RedirectToAction(actionName);
        }

        private IEnumerable<Game> GetGamesForPlayer()
        {
            var id = CurrentPlayer.PlayerId;

            var currentGames = _unitOfWork.All<Game>(g => g.DarkPlayer.PlayerId == id || g.LightPlayer.PlayerId == id);

            return currentGames.ToList();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Challenge()
        {
            if (CurrentPlayer == null)
                return RedirectToAction("RegisterPlayer", new { actionName = "Challenge" });
                //throw new Exception("This username does not have an associated CurrentPlayer. Please create one in the CurrentPlayer registration screen.");

            var playersToChallenge = _unitOfWork.All<Player>(p => p.PlayerId != CurrentPlayer.PlayerId);
            
            var currentChallenges =
                _unitOfWork.All<Challenge>(c => c.ChallengingPlayer.PlayerId == CurrentPlayer.PlayerId);

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
                    ChallengerId = c.ChallengingPlayer.PlayerId,
                    ChallengeTitle = c.Title,
                    DarkPlayerId = c.DarkPlayer.PlayerId,
                    LightPlayerId = c.LightPlayer.PlayerId

                }).ToList()
            };

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Challenge(CreateChallengeViewModel model)
        {
            var opponent = _unitOfWork.Find<Player>(model.OpponentId);

            var challenge = new Challenge()
            {
                ChallengingPlayer = CurrentPlayer,
                DarkPlayer = model.IsPlayerDark ? CurrentPlayer : opponent,
                LightPlayer = !model.IsPlayerDark ? CurrentPlayer : opponent,
                DateTime = DateTime.Now,
                Title = model.ChallengeTitle
            };

            _unitOfWork.Add(challenge);
            _unitOfWork.Commit();

            return RedirectToAction("Challenge");
        }
    }
}