﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SharpCentral;

namespace ChessSharp.ChessItems
{
    public class ChessPiece
    {
        private MoveValidate move = new MoveValidate();
        private bool firstMove { get; set; }
        public bool alive { get; private set; } //if a king has this false, game is over
        public int currentSquare { get; private set; }
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

        public ChessPiece(SharpCentral.Piece piece, int pieceIdent, int piecePosition, bool pieceAlive = true, bool pieceFirstMove = true)
        {

            currentSquare = piecePosition;
            alive = pieceAlive;
            firstMove = pieceFirstMove;
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
            firstMove = pieceFirstMove;
            inCheck = false;

            isLight = p > Piece.Empty ? true : false;
        }

        public bool legalMove(SharpCentral.Piece[] boardState, int newPosition, bool firstMove, bool inCheck = false)
        {
            bool legalMove = false;
            if (newPosition > 63 || newPosition < 0) //Keeps the pieces on the board
                return false;

            switch (p)
            {
                case SharpCentral.Piece.lPawn:
                    legalMove = move.checkLightPawn(boardState, currentSquare, newPosition, firstMove);
                    break;
                case SharpCentral.Piece.lKing:
                    legalMove = move.checkKing(boardState, currentSquare, newPosition, firstMove, inCheck);
                    break;
                case SharpCentral.Piece.lQueen:
                    legalMove = move.checkQueen(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.lKnight:
                    legalMove = move.checkKnight(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.lBishop:
                    legalMove = move.checkLightBishop(boardState, currentSquare, newPosition);
                    break;
                case SharpCentral.Piece.lRook:
                    legalMove = move.checkRook(boardState, currentSquare, newPosition, firstMove);
                    break;
                default:
                    legalMove = false;
                    break;
            }

            return legalMove;
        }


        public string getImagePath()
        {
         switch (p)
         {
             case SharpCentral.Piece.dPawn:
                 return "/Image/darkPawn.png";
                 //break;
             case SharpCentral.Piece.dKnight:
                 return "/Image/darkKnight.png";
                 //break;
             case SharpCentral.Piece.dBishop:
                 return "/Image/darkBishop.png"; ;
                 //break;
             case SharpCentral.Piece.dRook:
                 return "/Image/darkRook.png"; ;
                 //break;
             case SharpCentral.Piece.dQueen:
                 return "/Image/darkQueen.png";
                 //break;
             case SharpCentral.Piece.dKing:
                 return "/Image/darkKing.png";
                 //break;
             case SharpCentral.Piece.lPawn:
                 return "/Image/lightPawn.png";
                 //break;
             case SharpCentral.Piece.lKnight:
                 return "/Image/lightKnight.png";
                 //break;
             case SharpCentral.Piece.lBishop:
                 return "/Image/lightBishop.png"; ;
                 //break;
             case SharpCentral.Piece.lRook:
                 return "/Image/lightRook.png"; ;
                 //break;
             case SharpCentral.Piece.lQueen:
                 return "/Image/lightQueen.png";
                 //break;
             case SharpCentral.Piece.lKing:
                 return "/Image/lightKing.png";
                 //break;
             default:
                 return "";
                 //this should be a blank png?
                 //break;
         }

        } //end getImagePath
    } //end class
} //end namespace