using System.Collections.Generic;

namespace Chess.Data.Entities
{
    public interface IGame
    {
        long Id { get; set; }
        string Name { get; set; }
        int LightScore { get; set; }
        int DarkScore { get; set; }
        int MoveCount { get; set; }
        int MoveCountSinceProgress { get; set; } //Progress: Capture of a piece or movement of a pawn
        bool Complete { get; set; }

        ICollection<Move> Moves { get; set; }
        ICollection<Square> Squares { get; set; }

        ChessUser LightPlayer { get; set; }
        ChessUser DarkPlayer { get; set; }
        ChessUser WinnerPlayer { get; set; }
        Challenge Challenge { get; set; }
    }

    public class Game : IModifiable, IGame
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int LightScore { get; set; }
        public int DarkScore { get; set; }
        public int MoveCount { get; set; }
        public int MoveCountSinceProgress { get; set; } //Progress: Capture of a piece or movement of a pawn
        public bool Complete { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
        public virtual ICollection<Square> Squares { get; set; }

        public virtual ChessUser LightPlayer { get; set; }
        public virtual ChessUser DarkPlayer { get; set; }
        public virtual ChessUser WinnerPlayer { get; set; }
        public virtual Challenge Challenge { get; set; }
    }
}