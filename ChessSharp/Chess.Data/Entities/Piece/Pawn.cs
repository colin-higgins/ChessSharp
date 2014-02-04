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

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves(Square[][] board)
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
            return MoveCount == 0 
                && move.RowChange == LegalDirectionByTeam() * 2 
                && move.ColumnChange == 0
                && destinationPiece == null;
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            var defender = GetDestinationPiece(board, move);

            if (move.RowChange != LegalDirectionByTeam())
                if (!ValidOpeningPushWithNoDefender(defender, move))
                    return false;

            if (move.ColumnChange != 0)
            {
                if (Math.Abs(move.ColumnChange) > 1)
                    throw new Exception("You may not move horizontally with a pawn.");
                if (move.RowChange != LegalDirectionByTeam())
                    throw new Exception("You are moving in the wrong direction for this pawn's team.");
                if (defender == null)
                    if (!IsLegalEnPassant(board, move, pastMoves)) 
                        return false;

                ValidateNotAttackingSameTeam(board, move);

                return true;
            }

            if (defender != null)
                throw new Exception("There is a piece in the way!");

            return true;
        }

        private bool IsLegalEnPassant(Square[][] board, Move move, IEnumerable<Move> pastMoves)
        {
            if (pastMoves == null) return false;

            var lastMove = pastMoves.FirstOrDefault();

            if (lastMove == null) return false;

            var piece = GetDestinationPiece(board, lastMove);

            if (!(piece.PieceType == PieceType.Pawn
                  && lastMove.EndRow == move.EndRow - LegalDirectionByTeam()
                  && lastMove.EndColumn == move.EndColumn
                  && piece.MoveCount == 1))
            {
                return false;
            }
            return true;
        }
    }
}