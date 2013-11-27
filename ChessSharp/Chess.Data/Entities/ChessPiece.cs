using System.Collections.Generic;
using Chess.Data.Enum;

namespace Chess.Data.Entities
{
    public abstract class ChessPiece : IPiece, IModifiable
    {
        public int Id { get; set; }
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

        public PieceType GetPieceType()
        {
            return PieceType;
        }

        public abstract IEnumerable<Move> GetValidMoves();
        public abstract bool IsLegalMove(int column, int row);
        public abstract void Move(int column, int row);
    }
} 