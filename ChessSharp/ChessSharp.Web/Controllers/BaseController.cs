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
        protected readonly IUnitOfWork UnitOfWork;
        protected string Username;
        protected ChessUser CurrentUser;

        public BaseController()
        {
            UnitOfWork = new ChessContext();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            if (HttpContext != null)
                Username = HttpContext.User.Identity.Name;

            if (!String.IsNullOrEmpty(Username))
            {
                GetCurrentChessPlayer(Username);

                var challenges =
                    UnitOfWork.All<Challenge>(c => c.ChallengingPlayer.Id != CurrentUser.Id && c.Accepted == null)
                        .Where(c => c.LightPlayer.Id == CurrentUser.Id || c.DarkPlayer.Id == CurrentUser.Id);

                var openChallenges = challenges.Select(c => new ExistingChallengeViewModel()
                {
                    Accepted = false,
                    ChallengeTitle = c.Title,
                    DateTime = c.DateTime,
                    Id = c.ChallengeId,
                    Opponent = c.ChallengingPlayer,
                    OpponentTeam = c.LightPlayer == c.ChallengingPlayer ? Team.Light : Team.Dark
                }).ToList();

                var lightGames = UnitOfWork.All<Game>(g => g.LightPlayer == CurrentUser && !g.Complete);
                var darkGames = UnitOfWork.All<Game>(g => g.DarkPlayer == CurrentUser && !g.Complete);

                var openGames = new List<GamePreviewViewModel>();
                foreach (var game in lightGames)
                {
                    var model = AutoMapper.Mapper.Map<GamePreviewViewModel>(game);
                    model.IsPlayersTurn = game.MoveCount % 2 == 0;
                    model.OpponentName = model.DarkPlayerName;
                    openGames.Add(model);
                }
                foreach (var game in darkGames)
                {
                    var model = AutoMapper.Mapper.Map<GamePreviewViewModel>(game);
                    model.IsPlayersTurn = game.MoveCount % 2 == 1;
                    model.OpponentName = model.LightPlayerName;
                    openGames.Add(model);
                }

                Session["__OpenChallenges"] = openChallenges;
                Session["__OpenGames"] = openGames;
            }
        }

        private void GetCurrentChessPlayer(string username)
        {
            CurrentUser =
                UnitOfWork
                .All<ChessUser>(u => String.Equals(u.UserName, username, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();
        }
    }
}