using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Chess.Domain.Repositories;
using ChessSharp.Web.Models;

namespace ChessSharp.Web.Controllers
{
    [Authorize]
    public class HistoryController : BaseController
    {
        private readonly GameRepository _gameRepository;

        public HistoryController()
        {
            _gameRepository = new GameRepository(UnitOfWork);
        }

        [HttpGet]
        public ActionResult Games(int? id = null)
        {
            var completedGames = _gameRepository.GetUserGames(CurrentUser).ToArray();

            var model = new HistoricalGamesViewModel
            {
                Game = completedGames.FirstOrDefault(g => g.Id == id)
            };

            foreach (var game in completedGames)
            {
                var gameModel = AutoMapper.Mapper.Map<CompletedGameViewModel>(game);

                if (game.WinnerPlayer != null)
                    gameModel.Win = false;
                if (game.WinnerPlayer == CurrentPlayer)
                    gameModel.Win = true;

                model.Games.Add(gameModel);
            }

            return View(model);
        }
	}
}