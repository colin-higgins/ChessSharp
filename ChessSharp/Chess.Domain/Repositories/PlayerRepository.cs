using Chess.Data;
using Chess.Data.Entities;

namespace Chess.Domain.Repositories
{
    public class PlayerRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddFromUser(ChessUser entity)
        {
            var newPlayer = new Player()
            {
                ChessUserId = entity.Id,
                DisplayName = entity.UserName,
            };

            _unitOfWork.Add(newPlayer);
        }

        public void SaveOrUpdate(Player entity)
        {
            var attached = _unitOfWork.Exists(entity);

            if (entity.PlayerId > 0 && !attached)
                _unitOfWork.Attach(entity);
            else
                _unitOfWork.Add(entity);
        }

        public void Remove(Player entity)
        {
            _unitOfWork.Remove(entity);
        }
    }
}
