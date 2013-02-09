using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class Rook
    {
        private int scoreValue = 5;


        public int getScoreValue()
        {
            return scoreValue;
        }

        public Boolean checkMove(int oldPosition, int newPosition, Boolean isWhite, Boolean firstMove)
        {

            if (newPosition / 8 == oldPosition / 8)
            {
                return true;
            }
            else if ((oldPosition - newPosition) % 8 == 0)
            {
                return true;
            }

            return false; //if none of the legal moves were made

        }
    }
}