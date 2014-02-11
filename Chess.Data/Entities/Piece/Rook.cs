using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Rook : ChessPiece
    {
        public Rook()
        {
            ScoreValue = 500;
        }

        public override IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            var legalMoves = new List<Move>();

            if (!CurrentColumn.HasValue || !CurrentRow.HasValue || !Alive) return legalMoves;

            var column = CurrentColumn.Value;
            var row = CurrentRow.Value;

            var endPositions = new List<Tuple<int, int>>();

            GetVerticalMoves(board, column, row, endPositions);
            GetHorizontalMoves(board, row, column, endPositions);

            legalMoves.AddRange(endPositions.Select(p => SetupNewMove(p.Item1, p.Item2)));

            return legalMoves;
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            ValidateNotAttackingSameTeam(board, move);

            if (move.RowChange != 0 && move.ColumnChange != 0)
                throw new Exception("You only move horizontal or vertical with a rook.");
            if (HasCollision(board, move))
                throw new Exception("There is a piece between you and your destination.");

            return true;
        }
    }
}