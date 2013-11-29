using System;
using Chess.Data.Entities;

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
            var destination = board[move.EndRow][move.EndColumn];

            if (AttackingSameTeam(board, move) || Math.Abs(move.RowChange) > 1)
                return false;
            if (move.ColumnChange > 1)
                return IsLegalCastle(board, move);

            return !destination.TargetedByTeam(board, GetOppositeTeam());
        }

        public bool IsLegalCastle(Square[][] board, Move move)
        {
            var destination = board[move.EndRow][move.EndColumn];
            var direction = move.ColumnChange;
            var rook = board[move.EndRow][move.EndColumn + direction].ChessPiece;

            if (move.RowChange != 0 || MoveCount > 0 || destination.ChessPiece != null)
                return false;
            if (rook == null || rook.PieceType != Enum.PieceType.Rook || rook.MoveCount > 0)
                return false;
            if (HasCollision(board, move))
                return false;
            if (IsInCheck(board))
                return false;

            return !destination.TargetedByTeam(board, GetOppositeTeam());
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