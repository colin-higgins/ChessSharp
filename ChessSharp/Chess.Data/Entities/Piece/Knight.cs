﻿using System;
using System.Collections.Generic;
using Chess.Data.Entities;

namespace Chess.Data.Piece
{
    public class Knight : ChessPiece
    {
        public Knight()
        {
            ScoreValue = 325;
        }

        public override System.Collections.Generic.IEnumerable<Move> GetValidMoves()
        {
            throw new NotImplementedException();
        }

        public override bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null)
        {
            if (AttackingSameTeam(board, move))
                return false;
            if (!InBounds(move.EndRow, move.EndColumn))
                return false;
            if (Math.Abs(move.RowChange) + Math.Abs(move.ColumnChange) != 3) //L-movement
                return false;

            return true;
        }
    }
}