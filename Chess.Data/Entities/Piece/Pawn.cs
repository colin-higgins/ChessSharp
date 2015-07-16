using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data.Entities;
using Chess.Data.Enum;

namespace Chess.Data.Piece
{
    public class Pawn : ChessPiece
    {
        public Pawn()
        {
            ScoreValue = 100;
        }

        public override IEnumerable<Move> GetValidMoves(Square[][] board)
        {
            var legalMoves = new List<Move>();

            if (!CurrentColumn.HasValue || !CurrentRow.HasValue || !Alive) return legalMoves;

            var column = CurrentColumn.Value;
            var row = CurrentRow.Value;
            var newRow = row + LegalDirectionByTeam();

            var openingMove = SetupNewMove(newRow + 1, column);
            var normalMove = SetupNewMove(newRow, column);

            if (board[normalMove.EndRow][normalMove.EndColumn].ChessPiece == null)
            {
                legalMoves.Add(normalMove);

                if (ValidOpeningPushWithNoDefender(board, openingMove))
                    legalMoves.Add(openingMove);
            }

            if (column - 1 >= 0)
            {
                var attackLeft = SetupNewMove(newRow, column - 1);
                var leftOccupant = board[attackLeft.EndRow][attackLeft.EndColumn].ChessPiece;

                if (leftOccupant != null && leftOccupant.Team != Team)
                    legalMoves.Add(attackLeft);
            }

            if (column + 1 < 8)
            {
                var attackRight = SetupNewMove(newRow, column + 1);
                var rightOccupant = board[attackRight.EndRow][attackRight.EndColumn].ChessPiece;

                if (rightOccupant != null && rightOccupant.Team != Team)
                    legalMoves.Add(attackRight);
            }

            return legalMoves;
        }

        public bool ValidOpeningPushWithNoDefender(Square[][] board, Move move)
        {
            return MoveCount == 0
                && move.RowChange == LegalDirectionByTeam() * 2
                && move.ColumnChange == 0
                && board[move.EndRow - LegalDirectionByTeam()][move.EndColumn].ChessPiece == null
                && board[move.EndRow][move.EndColumn].ChessPiece == null;
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            var defender = GetDestinationPiece(board, move);

            if (move.RowChange != LegalDirectionByTeam())
                if (!ValidOpeningPushWithNoDefender(board, move))
                    return false;

            if (move.ColumnChange != 0)
            {
                CheckForMultipleColumnMovement(move);

                CheckForLegalDirection(move);

                if (defender == null && !IsLegalEnPassant(board, move, pastMoves))
                    return false;

                ValidateNotAttackingSameTeam(board, move);

                return true;
            }

            if (defender != null)
                throw new Exception("There is a piece in the way!");

            return true;
        }

        private void CheckForLegalDirection(Move move)
        {
            if (move.RowChange != LegalDirectionByTeam())
                throw new Exception("You are moving in the wrong direction for this pawn's team.");
        }

        private static void CheckForMultipleColumnMovement(Move move)
        {
            if (Math.Abs(move.ColumnChange) > 1)
                throw new Exception("You may not move horizontally with a pawn.");
        }

        private bool IsLegalEnPassant(Square[][] board, Move move, IEnumerable<Move> pastMoves)
        {
            if (pastMoves == null) return false;

            var lastMove = pastMoves.FirstOrDefault();

            if (lastMove == null) return false;

            var piece = GetDestinationPiece(board, lastMove);

            if (IsPawnsSecondMoveInProperDirection(move, lastMove, piece))
            {
                return true;
            }

            return false;
        }

        private bool IsPawnsSecondMoveInProperDirection(Move move, Entities.Move lastMove, ChessPiece piece)
        {
            return piece.PieceType == PieceType.Pawn
                   && lastMove.EndRow == move.EndRow - LegalDirectionByTeam()
                   && lastMove.EndColumn == move.EndColumn
                   && piece.MoveCount == 1;
        }
    }
}