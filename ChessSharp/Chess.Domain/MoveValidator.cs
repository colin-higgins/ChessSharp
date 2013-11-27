using System;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Data.Enum;

namespace Chess.Domain
{
    public class MoveValidator
    {
        private bool InBounds(int row, int column)
        {
            var rowInBounds = 0 <= row && row <= 7;
            var columnInBounds = (0 <= column  && column <= 7);
            return rowInBounds && columnInBounds;
        }

        public bool IsLegalMoveBishop(Square[][] board, Move move)
        {
            var attacker = board[move.StartColumn][move.StartRow].ChessPiece;
            var occupant = board[move.EndColumn][move.EndRow].ChessPiece;

            if (occupant.Team == attacker.Team)
                return false;
            if (Math.Abs(move.RowChange) != Math.Abs(move.ColumnChange))
                return false;
            if (HasCollision(board, move))
                return false;

            return true;
        }

        private bool IsLegalMoveKnight(Square[][] board, Move move)
        {
            var attacker = board[move.StartColumn][move.StartRow].ChessPiece;
            var occupant = board[move.EndColumn][move.EndRow].ChessPiece;

            if (occupant.Team == attacker.Team)
                return false;
            if (!InBounds(move.EndRow, move.EndColumn))
                return false;
            if (Math.Abs(move.RowChange) + Math.Abs(move.ColumnChange) != 3)
                return false;

            return true;
        }

        public bool IsLegalMoveRook(Square[][] board, Move move)
        {
            if (move.RowChange != 0 && move.ColumnChange != 0)
                return false;

            var attacker = board[move.StartColumn][move.StartRow].ChessPiece;
            var occupant = board[move.EndColumn][move.EndRow].ChessPiece;
            
            if (occupant.Team == attacker.Team)
                return false;
            if (HasCollision(board, move))
                return false;

            return true;
        } 

        public bool IsLegalMoveQueen(Square[][] board, Move move)
        {
            var attacker = board[move.StartColumn][move.StartRow].ChessPiece;
            var occupant = board[move.EndColumn][move.EndRow].ChessPiece;

            if (occupant.Team == attacker.Team)
                return false;
            if (HasCollision(board, move))
                return false;

            return true;
        }

        private int GetMovementModifier(int change)
        {
            return change > 0 ? 1 : change < 0 ? -1 : 0;
        }

        public bool IsLegalMoveKing(Square[][] board, Move move)
        {
            var attacker = board[move.StartColumn][move.StartRow].ChessPiece;
            var occupant = board[move.EndColumn][move.EndRow].ChessPiece;

            if (occupant.Team == attacker.Team || Math.Abs(move.RowChange) > 1)
                return false;
            if (move.ColumnChange > 1)
                return IsLegalCastle(board, move);

            var rowModifier = GetMovementModifier(move.RowChange);
            var columnModifier = GetMovementModifier(move.ColumnChange);

            return !IsInCheck(board, move.EndColumn, move.EndRow);
        }

        public bool IsInCheck(Square[][] board, int columnIndex, int rowIndex)
        {
            var attacked = board[columnIndex][rowIndex].ChessPiece;

            foreach (var row in board)
                foreach (var square in row)
                    if (square.ChessPiece != null && square.ChessPiece.Team != attacked.Team)
                        if (square.ChessPiece.IsLegalMove(columnIndex, rowIndex))
                            return true;

            return false;
        }

        public bool IsLegalCastle(Square[][] board, Move move)
        {
            var attacker = board[move.StartColumn][move.StartRow].ChessPiece;
            var occupant = board[move.EndColumn][move.EndRow].ChessPiece;

            if (IsInCheck(board, move.StartColumn, move.StartRow))
                return false;

            if (attacker.PieceType != PieceType.King || attacker.MoveCount > 0 || occupant != null)
                return false;
            
            if (move.StartColumn == board.Length - 1) //Dark King
            {
            }
            else if (move.StartColumn == 0) //Light King
            {
            }

            return !IsInCheck(board, move.EndColumn, move.EndRow);
        }

        public bool HasCollision(Square[][] board, Move move)
        {
            var rowModifier = GetMovementModifier(move.RowChange);
            var columnModifier = GetMovementModifier(move.ColumnChange);

            var row = move.StartRow + rowModifier;
            var column = move.StartColumn + columnModifier;

            while (row != move.EndRow || column != move.EndColumn)
            {
                if (!InBounds(row, column))
                    return true;
                if (board[column][row].ChessPiece != null)
                    return true; //collison

                row += rowModifier;
                column += columnModifier;
            }
            return false;   
        }
    }
}