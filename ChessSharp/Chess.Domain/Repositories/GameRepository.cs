using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chess.Data;
using Chess.Data.Entities;

namespace Chess.Domain.Repositories
{
    public class GameRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Game> GetUserGames(ChessUser user)
        {
            var players = _unitOfWork.All<Player>(p => p.ChessUser == user);

            return players.SelectMany(p => p.Games);
        } 

        public IEnumerable<Game> GetPlayerGames(Player player)
        {
            return player.Games;
        }
    }
}
