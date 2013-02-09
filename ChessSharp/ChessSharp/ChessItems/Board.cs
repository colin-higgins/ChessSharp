using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpCentral;

namespace ChessSharp.ChessItems
{
    public class Board //This class carries the board for an individual game - allows setting the square properties.
    {
        Square[] chessBoard = new Square[64];
        ChessPiece[] pieceChest = new ChessPiece[64];

        public bool placePiece(Int32 pieceIdentity, Int32 squareIndex, Int32 squareOldIndex)
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


        public bool PopulatePieceChest()
        {
            try
            {
                var fresh = new SharpCentral.FreshGame();

                for (var i = 0; i < 32; i++)
                {
                    var boardPosition = i < 16 ? i : i + 16; //Grabs only the opposite ends of the fresh board (pieces only)
                    pieceChest[i] = new ChessPiece(fresh.chessBoard[boardPosition], i, boardPosition);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PlaceChestPieces()
        {
            try
            {
                foreach (var p in pieceChest)
                {
                    chessBoard[p.currentSquare].setOccupant(p.id);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
