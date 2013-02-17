using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpCentral;

namespace ChessSharp.ChessItems
{
    public class ChessPiece
    {
        private MoveValidate move = new MoveValidate();
        private int moveCount { get; set; }
        public bool alive { get; private set; } //if a king has this false, game is over
        public int currentSquare { get; set; }
        public int id { get; private set; }
        private int scoreValue { get; set; }
        private int actionValue { get; set; } //this may be used to help the CPU attack with weaker pieces later on?
        private int attackValue { get; set; }
        private int defenseValue { get; set; }

        private bool inCheck; //for the king piece

        public bool IsLight { get { return isLight; } }
        private bool isLight;

        private SharpCentral.Piece p;
        public SharpCentral.Piece PieceType
        {
            get { return p; }
            set { p = value; isLight = p > Piece.Empty ? true : false; }
        }

        public ChessPiece(SharpCentral.Piece piece, int pieceIdent, int piecePosition, bool pieceAlive = true, int pieceMoves = 0)
        {

            currentSquare = piecePosition;
            alive = pieceAlive;
            moveCount = pieceMoves;
            p = piece;
            id = pieceIdent;

            switch (piece)
            {
                case SharpCentral.Piece.lPawn:
                    actionValue = 10;
                    scoreValue = 100;
                    break;
                case SharpCentral.Piece.lKnight:
                    actionValue = 8;
                    scoreValue = 320;
                    break;
                case SharpCentral.Piece.lBishop:
                    actionValue = 8;
                    scoreValue = 325;
                    break;
                case SharpCentral.Piece.lRook:
                    actionValue = 6;
                    scoreValue = 500;
                    break;
                case SharpCentral.Piece.lQueen:
                    actionValue = 3;
                    scoreValue = 1000;
                    break;
                case SharpCentral.Piece.lKing:
                    actionValue = 1;
                    scoreValue = 32767;
                    break;
                case SharpCentral.Piece.dPawn:
                    actionValue = 10;
                    scoreValue = -100;
                    break;
                case SharpCentral.Piece.dKnight:
                    actionValue = 8;
                    scoreValue = -320;
                    break;
                case SharpCentral.Piece.dBishop:
                    actionValue = 8;
                    scoreValue = -325;
                    break;
                case SharpCentral.Piece.dRook:
                    actionValue = 6;
                    scoreValue = -500;
                    break;
                case SharpCentral.Piece.dQueen:
                    actionValue = 3;
                    scoreValue = -1000;
                    break;
                case SharpCentral.Piece.dKing:
                    actionValue = 1;
                    scoreValue = -32767;
                    break;
                default:
                    scoreValue = 0;
                    actionValue = 0;
                    //piece = null; //piece is going to be set as something to dispose of
                    break;

            }

            alive = pieceAlive;
            inCheck = false;

            isLight = p > Piece.Empty ? true : false;
        }

        public void Die()
        {
            alive = false;
        }

        public bool legalMove(SharpCentral.Piece[] boardState, int newPosition)
        {
            bool legalMove = false;
            if (newPosition > 63 || newPosition < 0) //Keeps the pieces on the board
                return false;

            switch (PieceType)
            {
                case SharpCentral.Piece.lPawn:
                    legalMove = move.checkLightPawn(boardState, currentSquare, newPosition, moveCount > 0 ? false : true);
                    break;
                case SharpCentral.Piece.lKing:
                    legalMove = move.checkLightKing(boardState, currentSquare, newPosition, moveCount > 0 ? false : true, inCheck);
                    break;
                case SharpCentral.Piece.lQueen:
                    legalMove = move.checkLightQueen(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.lKnight:
                    legalMove = move.checkLightKnight(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.lBishop:
                    legalMove = move.checkLightBishop(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.lRook:
                    legalMove = move.checkLightRook(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.dPawn:
                    legalMove = move.checkDarkPawn(boardState, currentSquare, newPosition, moveCount > 0 ? false : true);
                    break;
                case SharpCentral.Piece.dKing:
                    legalMove = move.checkDarkKing(boardState, currentSquare, newPosition, moveCount > 0 ? false : true, inCheck);
                    break;
                case SharpCentral.Piece.dQueen:
                    legalMove = move.checkDarkQueen(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.dKnight:
                    legalMove = move.checkDarkKnight(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.dBishop:
                    legalMove = move.checkDarkBishop(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.dRook:
                    legalMove = move.checkDarkRook(boardState, currentSquare, newPosition);
                    break;
                default:
                    legalMove = false;
                    break;
            }

            return legalMove;
        }

    } //end class
} //end namespace