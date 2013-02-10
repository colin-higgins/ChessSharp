using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/*
*TODO:
*    Turn the dark and light pawn checks into an interface that handles different teams through inverting all of the numbers
 *    Add a specific check castle method - Should activate if the king moves 2 spaces.
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

        public bool checkLightKnight(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            bool legal = false;

            switch (Math.Abs(positionChange))
            {
                case 17:
                case -17:
                    legal = true;
                    break;
                case 15:
                case -15:
                    legal = true;
                    break;
                case 10:
                case -10:
                    legal = true;
                    break;
                case 6:
                case -6:
                    legal = true;
                    break;
                default:
                    legal = false; //if none of the legal moves were made
                    break;
            }

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal;
        }

        public bool checkDarkKnight(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            bool legal = false;

            switch (Math.Abs(positionChange))
            {
                case 17:
                case -17:
                    legal = true;
                    break;
                case 15:
                case -15:
                    legal = true;
                    break;
                case 10:
                case -10:
                    legal = true;
                    break;
                case 6:
                case -6:
                    legal = true;
                    break;
                default:
                    legal = false; //if none of the legal moves were made
                    break;
            }

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal;
        }

        public bool checkLightRook(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            int modifier = 1;
            bool legal = false;

            if (positionChange > 8) //Same row
            {
                modifier = d;
                legal = true;
            }
            else if (positionChange % 8 == 0) //same column
            {
                modifier = 8 * d;
                legal = true;
            }

            for (var i = oldPosition; i != newPosition; i += modifier)
                if (boardState[i] != SharpCentral.Piece.Empty)
                    return false;

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkDarkRook(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            int modifier = 1;
            bool legal = false;

            if (positionChange > 8) //Same row
            {
                modifier = d;
                legal = true;
            }
            else if (positionChange % 8 == 0) //same column
            {
                modifier = 8 * d;
                legal = true;
            }

            for (var i = oldPosition; i != newPosition; i += modifier)
                if (boardState[i] != SharpCentral.Piece.Empty)
                    return false;

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkLightQueen(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            int modifier = 1;
            bool legal = false;

            if (positionChange < 8)
            {
                modifier = d;
                legal = true;
            }
            else if (positionChange % 8 == 0)
            {
                modifier = 8 * d;
                legal = true;
            }
            else if (positionChange % 9 == 0)
            {
                modifier = 9 * d;
                legal = true;
            }
            else if (positionChange % 7 == 0)
            {
                modifier = 7 * d;
                legal = true;
            }

            for (var i = oldPosition; i != newPosition; i += modifier)
                if (boardState[i] != SharpCentral.Piece.Empty)
                    return false;

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkDarkQueen(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            int modifier = 1;
            bool legal = false;

            if (positionChange < 8)
            {
                modifier = d;
                legal = true;
            }
            else if (positionChange % 8 == 0)
            {
                modifier = 8 * d;
                legal = true;
            }
            else if (positionChange % 9 == 0)
            {
                modifier = 9 * d;
                legal = true;
            }
            else if (positionChange % 7 == 0)
            {
                modifier = 7 * d;
                legal = true;
            }

            for (var i = oldPosition; i != newPosition; i += modifier)
                if (boardState[i] != SharpCentral.Piece.Empty)
                    return false;

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkLightKing(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, bool inCheck, bool checkCheck = false)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            bool legal;

            switch (Math.Abs(positionChange))
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

            if (legal)
            {
                if (boardState[newPosition] > SharpCentral.Piece.Empty)
                    return false;

                for (var i = 0; i < 64; i++)
                {
                    switch (boardState[i]) //if anything in here is fulfilled, the move would put King in check
                    {
                        case SharpCentral.Piece.dPawn:
                            if (checkDarkPawn(boardState, i, newPosition, false))
                                legal = false;
                            break;
                        case SharpCentral.Piece.dBishop:
                            if (checkDarkBishop(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.dKnight:
                            if (checkDarkKnight(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.dRook:
                            if (checkDarkRook(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.dQueen:
                            if (checkDarkQueen(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.dKing:
                            if (!checkCheck) //Halts infinite loop
                                if (checkDarkKing(boardState, i, newPosition, true, true))
                                    legal = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            return legal;
        }

        public bool checkDarkKing(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, bool inCheck, bool checkCheck = false)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            bool legal;

            switch (Math.Abs(positionChange))
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

            if (legal)
            {
                if (boardState[newPosition] < SharpCentral.Piece.Empty)
                    return false;

                for (var i = 0; i < 64; i++)
                {
                    switch (boardState[i]) //if anything in here is fulfilled, the move would put King in check
                    {
                        case SharpCentral.Piece.lPawn:
                            if (checkLightPawn(boardState, i, newPosition, false))
                                legal = false;
                            break;
                        case SharpCentral.Piece.lBishop:
                            if (checkLightBishop(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.lKnight:
                            if (checkLightKnight(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.lRook:
                            if (checkLightRook(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.lQueen:
                            if (checkLightQueen(boardState, i, newPosition))
                                legal = false;
                            break;
                        case SharpCentral.Piece.lKing:
                            if (!checkCheck) //Halts infinite loop
                                if (checkLightKing(boardState, i, newPosition, true, true))
                                    legal = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            return legal;
        }

    }
}