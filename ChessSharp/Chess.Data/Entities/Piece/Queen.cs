using System;
using System.Collections.Generic;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Queen : ChessPiece
    {
        public Queen()
        {
            ScoreValue = 1000;
        }

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }

        private bool HasLegalMovementModifiers(Move move)
        {
            if (Math.Abs(move.RowChange) == Math.Abs(move.ColumnChange)) //diagonal
                return true;
            if (move.RowChange != 0 && move.ColumnChange == 0) //vertical
                return true;
            if (move.RowChange == 0 && move.ColumnChange != 0) //horizontal
                return true;

            return false;
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            if (AttackingSameTeam(board, move))
                return false;
            if (!HasLegalMovementModifiers(move))
                return false;
            if (HasCollision(board, move))
                return false;

            return true;
        }
    }
}