using System.Collections.Generic;
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
            return _unitOfWork.All<Game>(g => g.LightPlayer == user || g.DarkPlayer == user);
        } 
    }
}
