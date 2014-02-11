using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Queen : ChessPiece
    {
        public Queen()
        {
            ScoreValue = 1000;
        }

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            var legalMoves = new List<Move>();

            if (!CurrentColumn.HasValue || !CurrentRow.HasValue || !Alive) return legalMoves;

            var column = CurrentColumn.Value;
            var row = CurrentRow.Value;

            var endPositions = new List<Tuple<int, int>>();

            GetVerticalMoves(board, column, row, endPositions);
            GetHorizontalMoves(board, row, column, endPositions);
            GetDiagonalMoves(board, row, column, endPositions);

            legalMoves.AddRange(endPositions.Select(p => SetupNewMove(p.Item1, p.Item2)));

            return legalMoves;
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
            ValidateNotAttackingSameTeam(board, move);

            if (!HasLegalMovementModifiers(move))
                throw new Exception("You may move diagonal, vertical, or horizontal with a queen.");
            if (HasCollision(board, move))
                throw new Exception("There is a piece between your queen and your destination!");

            return true;
        }
    }
}