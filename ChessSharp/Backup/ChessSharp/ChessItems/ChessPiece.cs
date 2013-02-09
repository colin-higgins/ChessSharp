using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ChessSharp.ChessItems
{

    public class ChessPiece
    {

        private moveValidate move = new moveValidate();
        private Boolean firstMove { get; set; }
        private Boolean alive { get; set; } //if a king has this false, game is over
        private Int32 currentSquareIdent { get; set; }
        private Int32 pieceIdentity { get; set; }

        public ChessShared.pieceType PieceType { get; set; }
        public ChessShared.pieceColor Color { get; set; }

        private Int32 scoreValue { get; set; }
        private Int32 actionValue { get; set; } //this may be used to help the CPU attack with weaker pieces later on?
        private Int32 attackValue { get; set; }
        private Int32 defenseValue { get; set; }

        private Boolean inCheck; //for the king piece

        public ChessPiece(ChessShared.pieceType pieceType, Int32 pieceIdent, Int32 piecePosition, ChessShared.pieceColor color, Boolean pieceAlive = true, Boolean pieceFirstMove = true)
        {

            currentSquareIdent = piecePosition;
            alive = pieceAlive;
            firstMove = pieceFirstMove;
            PieceType = pieceType;
            pieceIdentity = pieceIdent;

            switch (pieceType)
            {
                case ChessShared.pieceType.pawn:
                    actionValue = 6;
                    scoreValue = 100;
                    break;
                case ChessShared.pieceType.knight:
                    actionValue = 3;
                    scoreValue = 320;
                    break;
                case ChessShared.pieceType.bishop:
                    actionValue = 3;
                    scoreValue = 325;
                    break;
                case ChessShared.pieceType.rook:
                    actionValue = 2;
                    scoreValue = 500;
                    break;
                case ChessShared.pieceType.queen:
                    actionValue = 1;
                    scoreValue = 1000;
                    break;
                case ChessShared.pieceType.king:
                    actionValue = 0;
                    scoreValue = 32767;
                    break;
                default:
                    scoreValue = 0;
                    actionValue = -1;
                    //piece = null; //piece is going to be set as something to dispose of
                    break;

            }

            Color = color;
            alive = pieceAlive;
            firstMove = pieceFirstMove;
            inCheck = false;
        }

        public Boolean legalMove(Int32 newPosition,
                                    Boolean pieceDiagRight = false, Boolean pieceDiagLeft = false, Boolean inCheck = false)
        {
            //This method will not check to see if pieces obstruct the path
            Boolean legalMove = false;
            if (newPosition > 63 || newPosition < 0) //Keeps the pieces on the board
                return false;

            switch (PieceType)
            {
                case ChessShared.pieceType.pawn:
                    legalMove = move.checkPawn(currentSquareIdent, newPosition, Color, firstMove, pieceDiagRight, pieceDiagLeft);
                    break;
                case ChessShared.pieceType.king:
                    legalMove = move.checkKing(currentSquareIdent, newPosition, firstMove, inCheck);
                    break;
                case ChessShared.pieceType.queen:
                    legalMove = move.checkQueen(currentSquareIdent, newPosition);
                    break;
                case ChessShared.pieceType.knight:
                    legalMove = move.checkKnight(currentSquareIdent, newPosition);
                    break;
                case ChessShared.pieceType.bishop:
                    legalMove = move.checkBishop(currentSquareIdent, newPosition);
                    break;
                case ChessShared.pieceType.rook:
                    legalMove = move.checkRook(currentSquareIdent, newPosition);
                    break;
                default:
                    legalMove = false;
                    break;
            }

            return legalMove;
        }


        public String getImagePath()
        {
            if (Color == ChessShared.pieceColor.black)
            {
                switch (PieceType)
                {
                    case ChessShared.pieceType.pawn:
                        return "/Image/darkPawn.png";
                        //break;
                    case ChessShared.pieceType.knight:
                        return "/Image/darkKnight.png";
                        //break;
                    case ChessShared.pieceType.bishop:
                        return "/Image/darkBishop.png"; ;
                        //break;
                    case ChessShared.pieceType.rook:
                        return "/Image/darkRook.png"; ;
                        //break;
                    case ChessShared.pieceType.queen:
                        return "/Image/darkQueen.png";
                        //break;
                    case ChessShared.pieceType.king:
                        return "/Image/darkKing.png";
                        //break;
                    default:
                        return "";
                        //this should be a blank png?
                        //break;

                }
            } //end if color == black
            else if (Color == ChessShared.pieceColor.white)
            {
                switch (PieceType)
                {
                    case ChessShared.pieceType.pawn:
                        return "/Image/lightPawn.png";
                        //break;
                    case ChessShared.pieceType.knight:
                        return "/Image/lightKnight.png";
                        //break;
                    case ChessShared.pieceType.bishop:
                        return "/Image/lightBishop.png"; ;
                        //break;
                    case ChessShared.pieceType.rook:
                        return "/Image/lightRook.png"; ;
                        //break;
                    case ChessShared.pieceType.queen:
                        return "/Image/lightQueen.png";
                        //break;
                    case ChessShared.pieceType.king:
                        return "/Image/lightKing.png";
                        //break;
                    default:
                        return "";
                        //this should be a blank png?
                        //break;
                }
            } //end if color == white
            return "";

        } //end getImagePath
    } //end class
} //end namespace