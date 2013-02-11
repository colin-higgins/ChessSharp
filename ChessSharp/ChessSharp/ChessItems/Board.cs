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
        public Square[] chessBoard { get; private set; }
        public ChessPiece[] pieceChest { get; private set; }

        /// <summary>
        /// Use for a brand new game with vanilla settings. 
        /// </summary>
        public Board() 
        {
            chessBoard = new Square[64];
            pieceChest = new ChessPiece[32];

            for (var i = 0; i < 64; i++)
            {
                chessBoard[i] = new Square(null);
            }

            PopulatePieceChest();

            PlaceChestPieces();
        }

        /// <summary>
        /// Use for a game loaded from persistance. 
        /// </summary>
        public Board(ChessPiece[] customChest)
        {
            chessBoard = new Square[64];

            for (var i = 0; i < 64; i++ )
            {
                chessBoard[i] = new Square(null);
            }

            pieceChest = customChest;

            PopulatePieceChest();

            PlaceChestPieces();
        }

        public bool MovePiece(int currentPositon, int newPosition)
        {
            bool success = false;

            var occupant = chessBoard[currentPositon].getOccupant();

            if (occupant != null)
            {
                var boardState = chessBoard.Select(sq => { var x = sq.getOccupant(); return x != null ? x.PieceType : Piece.Empty; }).ToArray();

                if (occupant.legalMove(boardState, newPosition))
                {
                    success = placePiece(currentPositon, newPosition);
                }
            }
            return success;
        }

        private bool placePiece(int currentPosition, int newPosition)
        {
            var aggressor = chessBoard[currentPosition].getOccupant();
            var victim = chessBoard[newPosition].getOccupant();

            if (newPosition >= 0 && newPosition <= 63)
            {
                chessBoard[currentPosition] = new Square(null);

                if (victim != null)
                    victim.Die();

                chessBoard[newPosition] = new Square(aggressor);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Square getSquare(int index)
        {
            return chessBoard[index];
        }

        public Boolean setSquareOccupant(int index, ChessItems.ChessPiece p)
        {
            if (chessBoard[index].getOccupant() == null) {
                chessBoard[index].setOccupant(p);
                return true;
            }
            else if (chessBoard[index].getOccupant().Equals(p))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //TODO: Move this method into the model
        private bool LoadSavedPieceChest(int gameId)
        {
            try
            {
                var mockSaved = new ChessPiece[32];
                var fresh = new SharpCentral.FreshGame();
                for (var i = 0; i < 32; i++)
                {
                    var boardPosition = i < 16 ? i : i + 16; //Grabs only the opposite ends of the fresh board (pieces only)
                    mockSaved[i] = new ChessPiece(fresh.chessBoard[boardPosition], i, boardPosition);
                }

                var savedChest = mockSaved.Where(piece => piece.alive).ToArray();

                for (var i = 0; i < 32; i++)
                {
                    pieceChest[i] = savedChest[i];
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool PopulatePieceChest()
        {
            try
            {
                var fresh = new SharpCentral.FreshGame();

                for (var i = 0; i < 32; i++)
                {
                    var boardPosition = i < 16 ? i : i + 32; //Grabs only the opposite ends of the fresh board (pieces only)
                    pieceChest[i] = new ChessPiece(fresh.chessBoard[boardPosition], i, boardPosition);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool PlaceChestPieces()
        {
            try
            {
                foreach (var p in pieceChest)
                {
                    chessBoard[p.currentSquare].setOccupant(p);
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
