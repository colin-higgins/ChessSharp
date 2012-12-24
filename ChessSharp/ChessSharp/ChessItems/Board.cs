using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessSharp.ChessItems
{
    public class Board //This class carries the board for an individual game - allows setting the square properties.
    {
        Square[] chessBoard = new Square[64];
        ChessPiece[] pieceChest = new ChessPiece[32];

        public Boolean placePiece(Int32 pieceIdentity, Int32 squareIndex, Int32 squareOldIndex)
        {
            if (squareIndex >= 0 && squareIndex <= 63)
            {
                chessBoard[squareOldIndex] = new Square(0);
                chessBoard[squareIndex] = new Square(pieceIdentity);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public Square getSquare(Int32 index)
        {
            return chessBoard[index];
        }
        public Boolean setSquareOccupant(Int32 index, Int32 pieceIdent)
        {
            if (chessBoard[index].getOccupant() == 0) {
            chessBoard[index].setOccupant(pieceIdent);
            return true;
            }
            else if (chessBoard[index].getOccupant() == pieceIdent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Boolean populateBoard()
        {
            //Int32 tempPieceIdent = 0;
            try
            {
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.rook, 1, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(1);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.rook, 2, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(2);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.knight, 3, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(3);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.knight, 4, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(4);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.bishop, 5, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(5);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.bishop, 6, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(6);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.queen, 7, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(7);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.king, 8, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(8);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 9, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(9);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 10, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(10);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 11, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(11);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 12, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(12);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 13, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(13);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 14, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(14);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 15, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(15);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 16, 0, ChessShared.pieceColor.white);
                chessBoard[0] = new Square(16);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.rook, 17, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(17);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.rook, 18, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(18);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.knight, 19, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(19);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.knight, 20, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(20);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.bishop, 21, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(21);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.bishop, 22, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(22);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.queen, 23, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(23);                                                 
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.king, 24, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(24);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 25, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(25);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 26, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(26);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 27, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(27);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 28, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(28);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 29, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(29);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 30, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(30);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 31, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(31);
                pieceChest[0] = new ChessPiece(ChessShared.pieceType.pawn, 32, 0, ChessShared.pieceColor.black);
                chessBoard[0] = new Square(32);

                return true;
                
            }
            catch (Exception ex)
            {
                throw (ex);
            }
                
        }


    }
}
