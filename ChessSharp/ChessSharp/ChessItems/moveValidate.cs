using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
*TODO:
*    Turn the dark and light pawn checks into an interface that handles different teams through inverting all of the numbers
*/

namespace ChessSharp.ChessItems
{
    public class MoveValidate
    {
        public bool checkLightPawn(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove)
        {
            int positionChange = newPosition - oldPosition;

            if (positionChange == 8 && boardState[newPosition] == SharpCentral.Piece.Empty)
                return true;
            else if (positionChange == 9)
                if (boardState[newPosition] < SharpCentral.Piece.Empty)
                    return true;
            else if (positionChange == 7)
                if (boardState[newPosition] < SharpCentral.Piece.Empty)
                    return true;
            else if (positionChange == 16)
                if (firstMove && boardState[oldPosition + 8] == SharpCentral.Piece.Empty)
                    return true;

            return false; //if none of the legal moves were made
        }

        public bool checkDarkPawn(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove)
        {
            int positionChange = newPosition - oldPosition;

                if (positionChange == -8 && boardState[newPosition] == SharpCentral.Piece.Empty)
                    return true;
                else if (positionChange == -9)
                    if (boardState[newPosition] > SharpCentral.Piece.Empty)
                        return true;
                else if (positionChange == -7)
                    if (boardState[newPosition] > SharpCentral.Piece.Empty)
                        return true;
                else if (positionChange == -16)
                    if (firstMove && boardState[oldPosition - 8] == SharpCentral.Piece.Empty)
                        return true;

            return false; //if none of the legal moves were made
        }

        public bool checkLightBishop(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;

            if (positionChange % 9 == 0)
            {
                for (var i = oldPosition; i < newPosition; i += (9 * d))
                {
                    if (boardState[i] != SharpCentral.Piece.Empty)
                        return false;
                }
                if (boardState[newPosition] <= SharpCentral.Piece.Empty)
                    return true;

                return false;
            }
            else if (positionChange % 7 == 0)
            {
                for (var i = oldPosition; i < newPosition; i += (7 * d))
                {
                    if (boardState[i] != SharpCentral.Piece.Empty)
                        return false;
                }
                if (boardState[newPosition] <= SharpCentral.Piece.Empty)
                    return true;
            }

            return false; //if none of the legal moves were made
        }

        public bool checkDarkBishop(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;

            if (positionChange % 9 == 0)
            {
                for (var i = oldPosition; i < newPosition; i += (9 * d))
                {
                    if (boardState[i] != SharpCentral.Piece.Empty)
                        return false;
                }
                if (boardState[newPosition] >= SharpCentral.Piece.Empty)
                    return true;

                return false;
            }
            else if (positionChange % 7 == 0)
            {
                for (var i = oldPosition; i < newPosition; i += (7 * d))
                {
                    if (boardState[i] != SharpCentral.Piece.Empty)
                        return false;
                }
                if (boardState[newPosition] >= SharpCentral.Piece.Empty)
                    return true;
            }

            return false; //if none of the legal moves were made
        }

        public bool checkKnight(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            bool legal;

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

        public bool checkRook(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove)
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

        public bool checkQueen(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
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

        public bool checkKing(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, bool inCheck)
        {
            int positionChange = newPosition - oldPosition;
            bool legal;

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