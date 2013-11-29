using System;
using System.Linq;
using Chess.Data.Entities;
using Enum = Chess.Data.Enum;

namespace Chess.Data.Piece
{
    public class King : ChessPiece
    {
        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsLegalMove(Square[][] board, Move move)
        {
            if (AttackingSameTeam(board, move) || Math.Abs(move.RowChange) > 1)
                return false;
            if (move.ColumnChange > 1)
                return IsLegalCastle(board, move);

            var rowModifier = GetMovementModifier(move.RowChange);
            var columnModifier = GetMovementModifier(move.ColumnChange);

            return !IsInCheck(board);
        }

        public bool IsLegalCastle(Square[][] board, Move move)
        {
            var occupant = GetDestinationPiece(board, move);
            var direction = move.ColumnChange;

            if (move.RowChange != 0 || MoveCount > 0 || occupant != null)
                return false;

            if (IsInCheck(board))
                return false;

            if (Team == Enum.Team.Dark)
            {
            }
            else if (Team == Enum.Team.Light)
            {
            }

            return !IsInCheck(board);
        }

        public bool IsInCheck(Square[][] board)
        {
            if (Alive && CurrentColumn.HasValue && CurrentRow.HasValue)
            {
                var possibleMove = new Move() { EndColumn = CurrentColumn.Value, EndRow = CurrentRow.Value };

                foreach (var row in board)
                    foreach (var square in row.Where(square => square.ChessPiece != null))
                    {
                        possibleMove.EndColumn = square.Column;
                        possibleMove.EndRow = square.Row;
                        if (square.ChessPiece.IsLegalMove(board, possibleMove))
                            return true;
                    }
            }
            return false;
        }
    }
}