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

        [Authorize]
        [HttpGet]
        public ActionResult Games(int? id = null)
        {
            if (CurrentUser != null)
            {
                var completedGames = _gameRepository.GetUserGames(CurrentUser)
                    .Where(g => g.Complete)
                    .OrderBy(g => g.Id)
                    .Reverse()
                    .ToList();

                var games = new List<CompletedGameViewModel>();

                foreach (var game in completedGames)
                {
                    var gameModel = AutoMapper.Mapper.Map<CompletedGameViewModel>(game);

                    if (game.WinnerPlayer != null)
                        gameModel.Win = false;
                    if (game.WinnerPlayer == CurrentUser)
                        gameModel.Win = true;

                    games.Add(gameModel);
                }

                return View(games);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}