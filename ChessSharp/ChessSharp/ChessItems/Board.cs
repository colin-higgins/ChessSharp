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
        public int LastSquare { get; private set; }

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

            for (var i = 0; i < 64; i++)
            {
                chessBoard[i] = new Square(null);
            }

            pieceChest = customChest;

            PopulatePieceChest();

            PlaceChestPieces();
        }

        public bool MovePiece(int currentPositon, int newPosition, int moveCount)
        {
            bool success = false;

            var occupant = chessBoard[currentPositon].getOccupant();

            if (occupant != null)
            {
                if (moveCount % 2 == 0 ? occupant.PieceType > Piece.Empty : occupant.PieceType < Piece.Empty)
                {
                    var boardState = chessBoard.Select(sq => { var x = sq.getOccupant(); return x != null ? x.PieceType : Piece.Empty; }).ToArray();

                    if (occupant.legalMove(boardState, newPosition, LastSquare))
                    {
                        success = placePiece(currentPositon, newPosition);
                        if (success)
                        {
                            LastSquare = newPosition;
                        }
                    }
                }
                else
                {
                    //Error: you can not move the other team's piece!
                }
            }
            return success;
        }

        private bool placePiece(int currentPosition, int newPosition, bool enPassant = false)
        {
            var aggressor = chessBoard[currentPosition].getOccupant();
            ChessPiece victim;

            if (!enPassant)
            {
                victim = chessBoard[newPosition].getOccupant();
            }
            else
            {
                //If it is en passant, we can assume the piece is a pawn, absolute value of 1
                victim = chessBoard[newPosition - (8 * (int)aggressor.PieceType)].getOccupant(); 
            }

            if (newPosition >= 0 && newPosition <= 63)
            {
                chessBoard[currentPosition].setOccupant(null);

                if (victim != null)
                {
                    victim.Die();
                }

                chessBoard[newPosition].setOccupant(aggressor);
                aggressor.currentSquare = newPosition;
                aggressor.lastSquare = currentPosition;
                aggressor.moveCount++;
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
            if (chessBoard[index].getOccupant() == null)
            {
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
