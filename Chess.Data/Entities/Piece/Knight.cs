using System;
using System.Collections.Generic;
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
                var newRow = position.Row;
                var newColumn = position.Column;

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

        private static HashSet<EndPosition> PossibleEndPositions(int column, int row)
        {
            var possibleEndPosition = new HashSet<EndPosition>
            {
                new EndPosition(column + 1, row + 2),
                new EndPosition(column + 1, row - 2),
                new EndPosition(column - 1, row + 2),
                new EndPosition(column - 1, row - 2),
                new EndPosition(column + 2, row + 1),
                new EndPosition(column + 2, row - 1),
                new EndPosition(column - 2, row + 1),
                new EndPosition(column - 2, row - 1),
            };

            return possibleEndPosition;
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            ValidateNotAttackingSameTeam(board, move);

            if (!InBounds(move.EndRow, move.EndColumn))
                throw new Exception("You have moved out of bounds!");
            if (!IsValidKnightMove(move)) //L-movement
                throw new Exception("You may only move in a proper 'L' pattern for a knight.");

            return true;
        }

        private static bool IsValidKnightMove(Move move)
        {
            var possibleMoves = PossibleEndPositions(move.StartColumn, move.StartRow);

            var moveIsPossible = possibleMoves.Any(m => m.Row == move.EndRow && m.Column == move.EndColumn);

            return moveIsPossible;
        }

        private class EndPosition
        {
            public EndPosition(int column, int row)
            {
                Column = column;
                Row = row;
            }

            public int Row { get; private set; }
            public int Column { get; private set; }
        }
    }
}