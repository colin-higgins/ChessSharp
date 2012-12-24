using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class Queen
    {
        private int scoreValue = 10;


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
            else if (((oldPosition - newPosition) % 9 == 0) || ((oldPosition - newPosition) % 7 == 0))
            {
                return true;
            }


            return false; //if none of the legal moves were made

        }
    }
}