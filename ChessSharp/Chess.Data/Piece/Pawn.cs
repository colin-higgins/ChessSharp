using System;
using Chess.Data.Entities;
using Chess.Data.Enum;

namespace Chess.Data.Piece
{
    public class Pawn : ChessPiece
    {
        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }

        private int LegalDirectionByTeam()
        {
            if (Team == Team.Light)
                return 1;
            return -1;
        }

        public bool ValidOpeningPushWithNoDefender(ChessPiece destinationPiece, Move move)
        {
            return MoveCount != 0 
                && move.RowChange != LegalDirectionByTeam() * 2 
                && move.ColumnChange != 0
                && destinationPiece == null;
        }

        public override bool IsLegalMove(Square[][] board, Move move)
        {
            var defender = GetDestinationPiece(board, move);

            if (move.RowChange != LegalDirectionByTeam())
                if (!ValidOpeningPushWithNoDefender(defender, move))
                    return false;

            if (move.ColumnChange != 0)
            {
                if (Math.Abs(move.ColumnChange) > 1)
                    return false;
                if (move.RowChange != LegalDirectionByTeam())
                    return false;
                if (defender == null)
                    return false;
                if (AttackingSameTeam(board, move))
                    return false;
                return true;
            }

            if (defender != null)
                return false;

            return true;
        }
    }
}