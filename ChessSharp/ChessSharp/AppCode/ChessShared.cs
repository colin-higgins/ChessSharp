using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp
{
    public class ChessShared
    {
        public enum pieceName
        {
            pawn = (int) 1,
            knight = (int) 2,
            bishop = (int) 3,
            rook = (int) 4,
            queen = (int) 5,
            king = (int) 6
        }


        public enum pieceType
        {
            pawn,
            knight,
            bishop,
            rook,
            queen,
            king
        }

        public enum pieceColor
        {
            white,
            black
        }
    }
}