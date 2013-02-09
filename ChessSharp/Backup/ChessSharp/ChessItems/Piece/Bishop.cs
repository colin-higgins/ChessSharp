using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class Bishop
    {
        private int scoreValue = 5;


        public int getScoreValue()
        {
            return scoreValue;
        }

        public Boolean checkMove(int oldPosition, int newPosition, Boolean isWhite, Boolean firstMove)
        {


            if (((oldPosition - newPosition) % 9 == 0) || ((oldPosition - newPosition) % 7 == 0))
            {
                return true;
            }


            return false; //if none of the legal moves were made

        }
    }
}