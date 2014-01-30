using System;
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
            ValidateNotAttackingSameTeam(board, move);

            if (move.RowChange != 0 && move.ColumnChange != 0)
                throw new Exception("You only move horizontally or vertically with a rook.");
            if (HasCollision(board, move))
                throw new Exception("There is a piece between you and your destination.");

            return true;
        }
    }
}