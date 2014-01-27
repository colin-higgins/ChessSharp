using Chess.Data.Enum;

namespace ChessSharp.Web.Models
{
    public abstract class ChessPieceViewModel
    {
        public int ChessPieceId { get; set; }
        public long GameId { get; set; }
        public int MoveCount { get; set; }
        public bool Alive { get; set; }
        public int? CurrentRow { get; set; }
        public int? CurrentColumn { get; set; }
        public int ScoreValue { get; set; }

        public Team Team { get; set; }
        public PieceType PieceType { get; set; }
    }
} 