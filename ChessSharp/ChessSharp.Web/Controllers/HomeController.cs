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
            if (CurrentPlayer == null)
                return RedirectToAction("RegisterPlayer", "Home", new { actionName = "Index", controllerName = "Home" });

            return View();
        }

        [Authorize]
        public ActionResult Play()
        {
            if (CurrentPlayer == null)
                return RedirectToAction("RegisterPlayer", "Home", new { actionName = "Play", controllerName = "Home" });

            var games = GetGamesForPlayer();

            var gamesViewModel = games.Select(g => new ActiveGameViewModel()
            {
                Id = g.Id,
                CurrentPlayerId = CurrentPlayer.PlayerId,
                DarkPlayerName = g.DarkPlayer.DisplayName,
                LightPlayerName = g.LightPlayer.DisplayName,
                Name = g.Name
            });

            return View(gamesViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RegisterPlayer(string actionName, string controllerName)
        {
            var repository = new PlayerRepository(UnitOfWork);
            repository.AddFromUser(CurrentUser);
            UnitOfWork.Commit();

            return RedirectToAction(actionName, controllerName);
        }

        private IEnumerable<Game> GetGamesForPlayer()
        {
            var id = CurrentPlayer.PlayerId;

            var currentGames = UnitOfWork.All<Game>(g => g.DarkPlayer.PlayerId == id || g.LightPlayer.PlayerId == id);

            return currentGames.ToList();
        }
    }
}