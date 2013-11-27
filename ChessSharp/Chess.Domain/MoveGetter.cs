using System;
using System.Collections.Generic;
using Chess.Data;

namespace Chess.Domain
{
    public static class MoveGetter
    {
        public static IEnumerable<Move> DiagonalMoves(int row, int column)
        {
            var moves = new List<Move>();
            Action<int, int> addMove = (r, c) => moves.Add(new Move()
            {
                StartColumn = column,
                StartRow = row,
                EndColumn = c,
                EndRow = r
            });

            var down = column;
            var up = column;

            for (var r = row; r < 8; r++)
            {
                if (--down > 0)
                    addMove(r, down);
                if (--up < 8)
                    addMove(r, up);
            }

            down = column;
            up = column;

            for (var r = row; r >= 0; r--)
            {
                if (--down > 0)
                    addMove(r, down);
                if (--up < 8)
                    addMove(r, up);
            }

            return moves;
        }

        public static IEnumerable<Move> AxisMoves(int row, int column)
        {
            var moves = new List<Move>();
            Action<int, int> addMove = (r, c) => moves.Add(new Move()
            {
                StartColumn = column,
                StartRow = row,
                EndColumn = c,
                EndRow = r
            });

            for (var r = 0; r < 8; r++)
                if (r != row)
                addMove(r, column);

            for (var c = 0; c < 8; c++)
                if (c != column)
                    addMove(row, c);

            return moves;
        }
    }
}
