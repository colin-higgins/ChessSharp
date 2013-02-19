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
        private bool LegalEnPassant(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, SharpCentral.team team, ChessPiece passedPiece)
        {
            bool enPassant = false;
            int d = team == SharpCentral.team.light ? -1 : 1;

            if (passedPiece.moveCount == 1) //The victim made it's first move last turn
            {
                //The square moved to is empty, and the square passed is the opposite team
                if ((int)boardState[newPosition] == (int)SharpCentral.Piece.Empty && (int)boardState[newPosition + 8 * d] * d == (int)SharpCentral.Piece.lPawn)
                {
                    //Verify victims current Square is behind attacker and that it is on the proper en passant row
                    if ((passedPiece.currentSquare == newPosition - 8) && (passedPiece.currentSquare / 8 == (team == SharpCentral.team.dark ? 3 : 4)))
                    {
                        enPassant = true;
                    }
                }
            }

            return enPassant;
        }
        private bool checkPawn(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, SharpCentral.team team, ChessPiece passedPiece = null)
        {
            int d = team == SharpCentral.team.light ? -1 : 1;

            int positionChange = (newPosition - oldPosition) * d;
            bool legal = false;

            switch (positionChange)
            {
                case 8:
                    if (boardState[newPosition] == SharpCentral.Piece.Empty)
                        legal = true;
                    break;
                case 9:
                case 7:
                    if ((int)boardState[newPosition] * d > (int)SharpCentral.Piece.Empty)
                    {
                        legal = true;
                    }
                    else if (LegalEnPassant(boardState, oldPosition, newPosition, team, passedPiece))
                    {
                        legal = true;
                    }
                    break;
                case 16:
                    if (firstMove && boardState[oldPosition + (8 * d)] == SharpCentral.Piece.Empty)
                        legal = true;
                    break;
                default:
                    legal = false;
                    break;
            }

            return legal; //if none of the legal moves were made
        }

        public bool checkLightPawn(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, int lastSquareEffected)
        {
            return checkPawn(boardState, oldPosition, newPosition, firstMove, SharpCentral.team.light, lastSquareEffected);
        }

        public bool checkDarkPawn(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, int lastSquareEffected)
        {
            return checkPawn(boardState, oldPosition, newPosition, firstMove, SharpCentral.team.dark, lastSquareEffected);
        }

        public bool checkBishop(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int modifier = 1;
            bool legal = false;
            int d = positionChange < 0 ? -1 : 1;

            if (positionChange % 9 == 0)
            {
                legal = true;
                modifier = 9 * d;
            }
            else if (positionChange % 7 == 0)
            {
                legal = true;
                modifier = 7 * d;
            }

            for (var i = oldPosition + modifier; i < newPosition; i += modifier)
            {
                if (boardState[i] != SharpCentral.Piece.Empty)
                    legal = false;
            }

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal;
        }

        public bool checkLightBishop(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkBishop(boardState, oldPosition, newPosition);

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal;
        }

        public bool checkDarkBishop(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkBishop(boardState, oldPosition, newPosition);

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal;
        }

        private bool checkKnight(int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            bool legal = false;

            switch (Math.Abs(positionChange))
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

        public bool checkLightKnight(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkKnight(oldPosition, newPosition);

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal &= false;

            return legal;
        }

        public bool checkDarkKnight(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkKnight(oldPosition, newPosition);

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal;
        }

        public bool checkRook(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            int modifier = 1;
            bool legal = false;

            if (sameRow(oldPosition, newPosition)) //Same row
            {
                modifier = d;
                legal = true;
            }
            else if (positionChange % 8 == 0) //same column
            {
                modifier = 8 * d;
                legal = true;
            }

            for (var i = oldPosition + modifier; i != newPosition; i += modifier)
                if (boardState[i] != SharpCentral.Piece.Empty)
                    return false;

            return legal; //if none of the legal moves were made
        }

        public bool checkLightRook(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {

            bool legal = false;

            legal = checkRook(boardState, oldPosition, newPosition);

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkDarkRook(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkRook(boardState, oldPosition, newPosition);

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkQueen(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            int positionChange = newPosition - oldPosition;
            int d = positionChange < 0 ? -1 : 1;
            int modifier = 1;
            bool legal = false;
            
            if (positionChange % 8 == 0)
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
            else if (sameRow(oldPosition, newPosition))
            {
                modifier = d;
                legal = true;
            }
            //0 to 7
            int rowLower = oldPosition % 8 - 7;
            int rowUpper = 7 - rowLower;
 
            for (var i = oldPosition + modifier; i != newPosition; i += modifier)
                if (boardState[i] != SharpCentral.Piece.Empty)
                    return false;

            return legal; //if none of the legal moves were made
        }

        private bool sameRow(int oldPosition, int newPosition)
        {
            bool same = false;
            //0 to 7
            int rowLower = oldPosition % 8 - 7;
            int rowUpper = 7 - rowLower;

            same = (rowLower < newPosition && newPosition < rowUpper);

            return same;
        }

        public bool checkLightQueen(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkQueen(boardState, oldPosition, newPosition);

            if (boardState[newPosition] > SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkDarkQueen(SharpCentral.Piece[] boardState, int oldPosition, int newPosition)
        {
            bool legal = false;

            legal = checkQueen(boardState, oldPosition, newPosition);

            if (boardState[newPosition] < SharpCentral.Piece.Empty)
                legal = false;

            return legal; //if none of the legal moves were made
        }

        public bool checkKing(int oldPosition, int newPosition, bool firstMove, bool inCheck)
        {
            int positionChange = newPosition - oldPosition;
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

            return legal;
        }

        public bool checkLightKing(SharpCentral.Piece[] boardState, int oldPosition, int newPosition, bool firstMove, bool inCheck, bool checkCheck = false)
        {
            int positionChange = newPosition - oldPosition;
            bool legal;

            legal = checkKing(oldPosition, newPosition, firstMove, inCheck);

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
            bool legal;

            legal = checkKing(oldPosition, newPosition, firstMove, inCheck);

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