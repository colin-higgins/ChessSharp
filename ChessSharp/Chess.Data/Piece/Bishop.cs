using System;
using System.Collections.Generic;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Bishop : ChessPiece
    {
        public override IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }


        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            if (AttackingSameTeam(board, move))
                return false;
            if (Math.Abs(move.RowChange) != Math.Abs(move.ColumnChange))
                return false;
            if (HasCollision(board, move))
                return false;

            return true;
        }
    }
}