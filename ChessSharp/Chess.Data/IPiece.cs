using System.Collections.Generic;
using Chess.Data.Enum;

namespace Chess.Data
{
    public interface IPiece
    {
        PieceType GetPieceType();
        IEnumerable<Move> GetValidMoves();
        bool IsLegalMove(int column, int row);
        void Move(int column, int row);
    }
}