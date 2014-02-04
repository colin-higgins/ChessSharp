using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
            var games = GetGamesForPlayer();

            var gamesViewModel = games.Select(g => new GamePreviewViewModel()
            {
                Id = g.Id,
                CurrentPlayerId = CurrentUser.Id,
                DarkPlayerName = g.DarkPlayer.DisplayName,
                LightPlayerName = g.LightPlayer.DisplayName,
                Name = g.Name
            });

            return View(gamesViewModel);
        }

        private IEnumerable<Game> GetGamesForPlayer()
        {
            var id = CurrentUser.Id;

            var currentGames = UnitOfWork.All<Game>(g => g.DarkPlayer.Id == id || g.LightPlayer.Id == id);

            return currentGames.ToList();
        }
    }
}