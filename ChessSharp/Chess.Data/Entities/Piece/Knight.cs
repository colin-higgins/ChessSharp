using System;
using System.Collections.Generic;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Knight : ChessPiece
    {
        public Knight()
        {
            ScoreValue = 325;
        }

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            throw new NotImplementedException();
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            ValidateNotAttackingSameTeam(board, move);

            if (!InBounds(move.EndRow, move.EndColumn))
                throw new Exception("You have moved out of bounds!");
            if (Math.Abs(move.RowChange) + Math.Abs(move.ColumnChange) != 3) //L-movement
                throw new Exception("You may only move in a proper 'L' pattern for a knight.");

            return true;
        }
    }
}