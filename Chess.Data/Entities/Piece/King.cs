﻿using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class King : ChessPiece
    {
        public King()
        {
            ScoreValue = Int32.MaxValue;
        }

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            var legalMoves = new List<Move>();

            if (!CurrentColumn.HasValue || !CurrentRow.HasValue || !Alive) return legalMoves;

            var column = CurrentColumn.Value;
            var row = CurrentRow.Value;

            var possibleEndPosition = new[]
            {
                new Tuple<int, int>(row, column + 1),
                new Tuple<int, int>(row, column - 1),
                new Tuple<int, int>(row + 1, column + 1),
                new Tuple<int, int>(row + 1, column - 1),
                new Tuple<int, int>(row - 1, column + 1),
                new Tuple<int, int>(row - 1, column - 1),
                new Tuple<int, int>(row + 1, column),
                new Tuple<int, int>(row - 1, column),
            };

            foreach (var position in possibleEndPosition)
            {
                var newRow = position.Item1;
                var newColumn = position.Item2;

                if (newRow < 8 && newRow >= 0)
                    if (newColumn < 8 && newColumn >= 0)
                    {
                        var occupant = board[newRow][newColumn].ChessPiece;
                        if (occupant == null || occupant.Team != Team)
                            legalMoves.Add(SetupNewMove(newRow, newColumn));
                    }
            }

            return legalMoves;
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            var destination = board[move.EndRow][move.EndColumn];

            ValidateNotAttackingSameTeam(board, move);

            if (Math.Abs(move.RowChange) > 1)
                throw new Exception("You may not move multiple columns.");
            if (move.ColumnChange > 1)
            {
                if (MoveCount > 0)
                    throw new Exception("You may not move multiple columns.");
                return IsLegalCastle(board, move);
            }

            return !destination.TargetedByTeam(board, GetOppositeTeam());
        }

        public bool IsLegalCastle(Square[][] board, Move move)
        {
            var source = board[move.StartRow][move.StartColumn];
            var destination = board[move.EndRow][move.EndColumn];
            var direction = GetMovementModifier(move.ColumnChange);

            var rook = direction > 0
                ? board[move.EndRow][7].ChessPiece
                : board[move.EndRow][0].ChessPiece;

            if (move.RowChange != 0)
                throw new Exception("Illegal move.");
            if (MoveCount > 0)
                throw new Exception("You may only castle if your king has not moved yet.");
            if (destination.ChessPiece != null)
                throw new Exception("You may not castle to an occupied square.");
            if (rook == null || rook.PieceType != Data.Enum.PieceType.Rook || rook.MoveCount > 0)
                throw new Exception("The rook on that side has already moved.");
            if (HasCollision(board, move))
                throw new Exception("There is a piece in the way of the castle.");
            if (IsInCheck(board))
                throw new Exception("You may not castle while in check.");
            if (destination.TargetedByTeam(board, GetOppositeTeam()))
                throw new Exception("You may not castle into check.");

            return true;
        }

        public bool IsInCheck(Square[][] board)
        {
            if (Alive && CurrentColumn.HasValue && CurrentRow.HasValue)
            {
                var square = board[CurrentRow.Value][CurrentColumn.Value];

                var team = GetOppositeTeam();

                if (square.TargetedByTeam(board, team))
                    return true;
            }
            return false;
        }
    }
}
