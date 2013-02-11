using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessSharp.ChessItems
{
    public class Square
    {
        private ChessItems.ChessPiece occupant; //if null, there is no occupant
        //private Boolean occupied;

        public Square(ChessItems.ChessPiece piece)
        {
            setOccupant(piece);
        }

        //occupant getter setter
        public void setOccupant(ChessItems.ChessPiece piece)
        {
            occupant = piece;
            //if (pieceIdentity == 0)
            //    occupied = false;
            //else
            //    occupied = true;
        }
        public ChessItems.ChessPiece getOccupant()
        {
            return occupant;
        }



    }
}
