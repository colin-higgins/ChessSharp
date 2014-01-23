using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Data.Enum;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected string Username;
        protected Player CurrentPlayer;

        public BaseController()
        {
            _unitOfWork = new ChessContext();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (HttpContext != null)
                Username = HttpContext.User.Identity.Name;

            if (!String.IsNullOrEmpty(Username))
            {
                CurrentPlayer = GetCurrentChessPlayer();

                var challenges =
                    _unitOfWork.All<Challenge>(c => c.ChallengingPlayerId != CurrentPlayer.PlayerId && c.Accepted == null)
                        .Where(c => c.LightPlayerId == CurrentPlayer.PlayerId || c.DarkPlayerId == CurrentPlayer.PlayerId);

                var openChallenges = challenges.Select(c => new ExistingChallengeViewModel()
                {
                    Accepted = false,
                    ChallengeTitle = c.Title,
                    DateTime = c.DateTime,
                    Id = c.ChallengeId,
                    Opponent = c.ChallengingPlayer,
                    OpponentTeam = c.LightPlayer == c.ChallengingPlayer ? Team.Light : Team.Dark
                }).ToList();

                Session["__OpenChallenges"] = openChallenges;
            }
        }

        private Player GetCurrentChessPlayer()
        {
            var username = HttpContext.User.Identity.Name;
            var currentChessUser = _unitOfWork
                .All<Player>(u => String.Equals(u.ChessUser.UserName, username, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
            return currentChessUser;
        }
    }
}