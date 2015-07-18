using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Knight : ChessPiece
    {
        public Knight()
        {
            ScoreValue = 325;
        }

        public override IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            var legalMoves = new List<Move>();

            if (!CurrentColumn.HasValue || !CurrentRow.HasValue || !Alive) return legalMoves;

            var column = CurrentColumn.Value;
            var row = CurrentRow.Value;

            var possibleEndPositions = PossibleEndPositions(column, row);

            foreach (var position in possibleEndPositions)
            {
                var newRow = position.Item1;
                var newColumn = position.Item2;

                if (EndPositionIsWithinBounds(newRow, newColumn))
                {
                    var occupant = board[newRow][newColumn].ChessPiece;

                    if (occupant == null || occupant.Team != Team)
                        legalMoves.Add(SetupNewMove(newRow, newColumn));
                }
            }

            return legalMoves;
        }

        private static bool EndPositionIsWithinBounds(int newRow, int newColumn)
        {
            return newRow < 8 && newRow >= 0 && newColumn < 8 && newColumn >= 0;
        }

        private static Tuple<int, int>[] PossibleEndPositions(int column, int row)
        {
            var possibleEndPosition = new[]
            {
                new Tuple<int, int>(column + 1, row + 2),
                new Tuple<int, int>(column + 1, row - 2),
                new Tuple<int, int>(column - 1, row + 2),
                new Tuple<int, int>(column - 1, row - 2),
                new Tuple<int, int>(column + 2, row + 1),
                new Tuple<int, int>(column + 2, row - 1),
                new Tuple<int, int>(column - 2, row + 1),
                new Tuple<int, int>(column - 2, row - 1),
            };
            return possibleEndPosition;
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