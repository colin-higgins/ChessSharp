using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Bishop : ChessPiece
    {
        public Bishop()
        {
            ScoreValue = 300;
        }

        public override IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            var legalMoves = new List<Move>();

            if (!CurrentColumn.HasValue || !CurrentRow.HasValue || !Alive) return legalMoves;

            var column = CurrentColumn.Value;
            var row = CurrentRow.Value;

            var endPositions = new List<Tuple<int, int>>();

            GetDiagonalMoves(board, row, column, endPositions);

            legalMoves.AddRange(endPositions.Select(p => SetupNewMove(p.Item1, p.Item2)));

            return legalMoves;
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