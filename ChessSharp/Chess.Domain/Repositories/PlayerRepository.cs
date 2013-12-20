using Chess.Data;

namespace Chess.Domain.Repositories
{
    public class PlayerRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
