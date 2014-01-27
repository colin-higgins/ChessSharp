using System;
using System.Collections.Generic;
using System.IO;
using Chess.Data.Entities;
using Chess.Data.Enum;
using System.Linq;
using Chess.Data.Piece;

namespace Chess.Data
{
    public class Board
    {
        public Square[][] Squares { get; set; }

        public Board(Square[][] board)
        {
            Squares = board;
        }

        public Board(Square[] squares)
        {
            if (squares.Count() != 64)
                throw new InvalidDataException("There must be 64 squares to initialize a board.");

            var orderedSquares = squares.OrderBy(s => s.Row).ThenBy(s => s.Column);

            Squares = new[]
                {
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray(),
                    orderedSquares.Take(8).ToArray()
                };
        }

        public Board()
        {
            var row = 0;

            Squares = new[] {
                BackRow(row++, Team.Light),
                PawnRow(row++, Team.Light), 
                EmptyRow(row++),
                EmptyRow(row++),
                EmptyRow(row++),
                EmptyRow(row++),
                PawnRow(row++, Team.Dark), 
                BackRow(row, Team.Dark),
            };
        }

        private static Square NewSquare(int column, int row, ChessPiece piece)
        {
            if (piece != null)
            {
                piece.CurrentColumn = column;
                piece.CurrentRow = row;
                piece.Alive = true;
            }

            return new Square { Column = column, Row = row, ChessPiece = piece };
        }

        private static Square EmptySquare(int column, int row)
        {
            return NewSquare(column, row, null);
        }

        private static Square[] PawnRow(int row, Team team)
        {
            var squares = new List<Square>();

            for (var c = 0; c < 8; c++)
                squares.Add(NewSquare(c, row, new Pawn() { Team = team, PieceType = PieceType.Pawn }));

            return squares.ToArray();
        }

        private static Square[] BackRow(int row, Team team)
        {
            var c = 0; // fires closure on ref parameter

            Func<Team, Square> rook = t => NewSquare(c++, row, new Rook() { Team = t, PieceType = PieceType.Rook });
            Func<Team, Square> knight = t => NewSquare(c++, row, new Knight() { Team = t, PieceType = PieceType.Knight });
            Func<Team, Square> bishop = t => NewSquare(c++, row, new Bishop() { Team = t, PieceType = PieceType.Bishop });
            Func<Team, Square> queen = t => NewSquare(c++, row, new Queen() { Team = t, PieceType = PieceType.Queen });
            Func<Team, Square> king = t => NewSquare(c++, row, new King() { Team = t, PieceType = PieceType.King });

            return new[]
                {
                    rook(team), knight(team), bishop(team), 
                    queen(team), king(team),
                    bishop(team), knight(team), rook(team)
                };

        }

        private static Square[] EmptyRow(int row)
        {
            var squares = new List<Square>();

            for (var c = 0; c < 8; c++)
                squares.Add(EmptySquare(c, row));

            return squares.ToArray();
        }

    }
}
