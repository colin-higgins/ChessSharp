using System;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Knight : ChessPiece
    {
        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new NotImplementedException();
        }

        public override bool IsLegalMove(int column, int row)
        {
            throw new NotImplementedException();
        }

        public override void Move(int column, int row)
        {
            throw new NotImplementedException();
        }
    }
}