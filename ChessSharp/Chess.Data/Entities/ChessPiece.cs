using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Chess.Data.Enum;

namespace Chess.Data.Entities
{
    public abstract class ChessPiece : IPiece, IModifiable
    {
        [Key]
        public int ChessPieceId { get; set; }
        public long GameId { get; set; }
        public int MoveCount { get; set; }
        public bool Alive { get; set; }
        public int? CurrentRow { get; set; }
        public int? CurrentColumn { get; set; }
        public int ScoreValue { get; set; }
        public int ActionValue { get; set; } //this may be used to help the CPU attack with weaker pieces later on?
        public int AttackValue { get; set; }
        public int DefenseValue { get; set; }

        public Team Team { get; set; }
        public PieceType PieceType { get; set; }

        public virtual Game Game { get; set; }

        public PieceType GetPieceType()
        {
            return PieceType;
        }

        protected ChessPiece GetAttacker(Square[][] board, Move move)
        {
            return board[move.StartRow][move.StartColumn].ChessPiece;
        }

        protected Team GetOppositeTeam()
        {
            if (Team == Team.Light)
                return Team.Dark;

            return Team.Light;
        }

        protected ChessPiece GetDestinationPiece(Square[][] board, Move move)
        {
            return board[move.EndRow][move.EndColumn].ChessPiece;
        }

        protected bool AttackingSameTeam(Square[][] board, Move move)
        {
            var attacker = GetAttacker(board, move);
            var occupant = GetDestinationPiece(board, move);

            if (occupant == null || attacker == null)
                return false;

            return occupant.Team == attacker.Team;
        }

        protected bool InBounds(int row, int column)
        {
            var rowInBounds = 0 <= row && row <= 7;
            var columnInBounds = (0 <= column && column <= 7);
            return rowInBounds && columnInBounds;
        }
        
        protected int GetMovementModifier(int change)
        {
            return change > 0 ? 1 : change < 0 ? -1 : 0;
        }

        protected bool HasCollision(Square[][] board, Move move)
        {
            var rowModifier = GetMovementModifier(move.RowChange);
            var columnModifier = GetMovementModifier(move.ColumnChange);

            var row = move.StartRow + rowModifier;
            var column = move.StartColumn + columnModifier;

            while (row != move.EndRow || column != move.EndColumn)
            {
                if (!InBounds(row, column))
                    return true; //out of bounds
                if (board[row][column].ChessPiece != null)
                    return true; //collison

                row += rowModifier;
                column += columnModifier;
            }
            return false;
        }
        
        public abstract IEnumerable<Move> GetValidMoves();
        public abstract bool IsLegalMove(Square[][] board, Move move, IEnumerable<Move> pastMoves = null);

        public void Move(Square[][] board, Move move)
        {
            var occupant = GetDestinationPiece(board, move);

            if (occupant != null && occupant.PieceType != PieceType.Empty)
            {
                occupant.Alive = false;
                occupant.CurrentColumn = null;
                occupant.CurrentRow = null;
            }

            board[move.EndRow][move.EndColumn].ChessPiece = this;
            board[move.StartRow][move.StartColumn].ChessPiece = null;

            CurrentColumn = move.EndRow;
            CurrentRow = move.EndRow;
            MoveCount++;
        }
    }
} 