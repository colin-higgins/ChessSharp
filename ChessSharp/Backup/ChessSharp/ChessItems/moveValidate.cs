using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessSharp.ChessItems
{
    public class moveValidate
    {
        public Boolean checkPawn(Int32 oldPosition, Int32 newPosition, ChessShared.pieceColor color, Boolean firstMove,
                                        Boolean pieceDiagRight, Boolean pieceDiagLeft)
        {

            Int32 positionChange = newPosition - oldPosition;

            if (color.Equals(ChessSharp.ChessShared.pieceColor.white))
            {
                if (positionChange == 8)
                    return true;
                else if (pieceDiagRight)
                {
                    if (positionChange == 9)
                        return true;
                }
                else if (pieceDiagLeft)
                {
                    if (positionChange == 7)
                        return true;
                }
                else if (firstMove)
                    if (positionChange == 16)
                        return true;

            }
            else if (color.Equals(ChessSharp.ChessShared.pieceColor.black)) //if the piece is black
            {
                if (positionChange == -8)
                    return true;
                else if (pieceDiagRight)
                {
                    if (positionChange == -9)
                        return true;
                }
                else if (pieceDiagLeft)
                {
                    if (positionChange == -7)
                        return true;
                }
                else if (firstMove)
                    if (positionChange == -16)
                        return true;
            }

            return false; //if none of the legal moves were made
        }

        public Boolean checkBishop(Int32 oldPosition, Int32 newPosition)
        {

            if (((oldPosition - newPosition) % 9 == 0) || ((oldPosition - newPosition) % 7 == 0))
            {
                return true;
            }

            return false; //if none of the legal moves were made

        }

        public Boolean checkKnight(Int32 oldPosition, Int32 newPosition)
        {
            Int32 positionChange = newPosition - oldPosition;
            Boolean legal;

            switch ( Math.Abs(positionChange) )
            {
                case 17:
                    legal = true;
                    break;
                case 15:
                    legal = true;
                    break;
                case 10:
                    legal = true;
                    break;
                case 6:
                    legal = true;
                    break;
                default:
                    legal = false; //if none of the legal moves were made
                    break;
            }

            return legal;
        }

        public Boolean checkRook(Int32 oldPosition, Int32 newPosition)
        {

            if (newPosition / 8 == oldPosition / 8)
            {
                return true;
            }
            else if ((oldPosition - newPosition) % 8 == 0)
            {
                return true;
            }

            //TODO: add support for castle move
            return false; //if none of the legal moves were made

        }

        public Boolean checkQueen(Int32 oldPosition, Int32 newPosition)
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

        public Boolean checkKing(Int32 oldPosition, Int32 newPosition, Boolean firstMove, Boolean inCheck)
        {
            Int32 positionChange = newPosition - oldPosition;
            Boolean legal;

            if (firstMove && !inCheck) //This allows the castling of either side
            {
                if (positionChange == 2)
                    return true;
                else if (positionChange == -2)
                    return true;
            }

            switch ( Math.Abs(positionChange) )
            {
                case 9:
                    legal = true;
                    break;
                case 8:
                    legal = true;
                    break;
                case 7:
                    legal = true;
                    break;
                case 1:
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