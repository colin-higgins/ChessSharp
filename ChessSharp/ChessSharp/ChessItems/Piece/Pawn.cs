using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class Pawn
    {
        private int scoreValue = 1;


        public int getScoreValue()
        {
            return scoreValue;
        }

        public Boolean checkMove(int oldPosition, int newPosition, Boolean isWhite, Boolean firstMove, 
                                        Boolean pieceDiagRight = false, Boolean pieceDiagLeft = false)
        {
            if (isWhite)
            {
                if (newPosition - oldPosition == 8)
                    return true;
                else if (pieceDiagRight)
                {
                    if (newPosition - oldPosition == 9)
                        return true;
                }
                else if (pieceDiagLeft)
                {
                    if (newPosition - oldPosition == 7)
                        return true;
                }
                else if (firstMove)
                    if (newPosition - oldPosition == 16)
                        return true;

            }
            else if (!isWhite) //if the piece is black
            {
                if (oldPosition - newPosition == 8)
                    return true;
                else if (pieceDiagRight)
                {
                    if (oldPosition - newPosition == 9)
                        return true;
                }
                else if (pieceDiagLeft)
                {
                    if (oldPosition - newPosition == 7)
                        return true;
                }
                else if (firstMove)
                    if (oldPosition - newPosition == 16)
                        return true;
            }

            return false; //if none of the legal moves were made
        }

    }
}