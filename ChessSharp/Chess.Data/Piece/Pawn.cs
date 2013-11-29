using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Pawn : ChessPiece
    {
        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsLegalMove(Square[][] board, Move move)
        {
            if (AttackingSameTeam(board, move))
                return false;
        }
    }
}