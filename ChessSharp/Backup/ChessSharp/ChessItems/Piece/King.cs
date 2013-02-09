using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class King
    {
        private int scoreValue = 1000; //The winning piece...


        public int getScoreValue()
        {
            return scoreValue;
        }

        public Boolean checkMove(int oldPosition, int newPosition, Boolean isWhite, Boolean firstMove, Boolean inCheck)
        {
            int positionChange = newPosition - oldPosition;
            Boolean legal;

            if (firstMove && !inCheck) //This allows the castling of either side
            {
                if (positionChange == 2)
                    return true;
                else if (positionChange == -2)
                    return true;
            }

            switch (positionChange)
            {
                case 9:
                    legal = true;
                    break;
                case -9:
                    legal = true;
                    break;
                case 8:
                    legal = true;
                    break;
                case -8:
                    legal = true;
                    break;
                case 7:
                    legal = true;
                    break;
                case -7:
                    legal = true;
                    break;
                case 1:
                    legal = true;
                    break;
                case -1:
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