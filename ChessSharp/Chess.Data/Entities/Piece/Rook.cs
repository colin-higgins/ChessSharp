using System.Collections.Generic;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Rook : ChessPiece
    {
        public Rook()
        {
            ScoreValue = 500;
        }

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            if (AttackingSameTeam(board, move))
                return false;
            if (move.RowChange != 0 && move.ColumnChange != 0)
                return false;
            if (HasCollision(board, move))
                return false;

            return true;
        }
    }
}