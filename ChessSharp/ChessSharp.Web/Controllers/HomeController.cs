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
            if (CurrentPlayer == null)
                return RedirectToAction("RegisterPlayer", new { actionName = "Play" });

            var games = GetGamesForPlayer();

            var gamesViewModel = games.Select(g => new ActiveGameViewModel()
            {
                GameId = g.GameId,
                CurrentPlayerId = CurrentPlayer.PlayerId,
                DarkPlayerName = g.DarkPlayer.DisplayName,
                LightPlayerName = g.LightPlayer.DisplayName,
                Name = g.Name
            });

            return View(gamesViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RegisterPlayer(string actionName)
        {
            var repository = new PlayerRepository(UnitOfWork);
            repository.AddFromUser(CurrentUser);
            UnitOfWork.Commit();

            return RedirectToAction(actionName);
        }

        private IEnumerable<Game> GetGamesForPlayer()
        {
            var id = CurrentPlayer.PlayerId;

            var currentGames = UnitOfWork.All<Game>(g => g.DarkPlayer.PlayerId == id || g.LightPlayer.PlayerId == id);

            return currentGames.ToList();
        }
    }
}