using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class Knight
    {
        private int scoreValue = 3; //The winning piece...


        public int getScoreValue()
        {
            return scoreValue;
        }

        public Boolean checkMove(int oldPosition, int newPosition, Boolean isWhite, Boolean firstMove, Boolean inCheck)
        {
            int positionChange = newPosition - oldPosition;
            Boolean legal;

            switch (positionChange)
            {
                case 17:
                    legal = true;
                    break;
                case -17:
                    legal = true;
                    break;
                case 15:
                    legal = true;
                    break;
                case -15:
                    legal = true;
                    break;
                case 10:
                    legal = true;
                    break;
                case -10:
                    legal = true;
                    break;
                case 6:
                    legal = true;
                    break;
                case -6:
                    legal = true;
                    break;
                default:
                    legal = false; //if none of the legal moves were made
                    break;
            }

            return legal;
        }
    }
}