using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessSharp.ChessItems
{
    public class Square
    {
        private Int32 occupant; //if zero, there is no occupant
        //private Boolean occupied;

        public Square(Int32 pieceIdentity)
        {
            setOccupant(pieceIdentity);
        }

        //occupant getter setter
        public void setOccupant(Int32 pieceIdentity)
        {
            occupant = pieceIdentity;
            //if (pieceIdentity == 0)
            //    occupied = false;
            //else
            //    occupied = true;
        }
        public Int32 getOccupant()
        {
            return occupant;
        }



    }
}
