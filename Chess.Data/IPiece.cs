using System.Collections.Generic;
using Chess.Data.Entities;
using Chess.Data.Enum;

namespace Chess.Data
{
    public interface IPiece
    {
        PieceType GetPieceType();
        IEnumerable<Move> GetValidMoves(Square[][] board);
        bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null);
        void Move(Square[][] board, Move move);
    }
}