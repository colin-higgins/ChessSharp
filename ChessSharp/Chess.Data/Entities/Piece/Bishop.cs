using System;
using System.Collections.Generic;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Bishop : ChessPiece
    {
        public Bishop()
        {
            ScoreValue = 300;
        }

        public override IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            ValidateNotAttackingSameTeam(board, move);

            if (Math.Abs(move.RowChange) != Math.Abs(move.ColumnChange))
                throw new Exception("You may only move diagonally with a bishop.");
            if (HasCollision(board, move))
                throw new Exception("There is a piece between your bishop and your destination!");

            return true;
        }
    }
}