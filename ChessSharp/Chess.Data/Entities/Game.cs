using System.Collections.Generic;

namespace Chess.Data.Entities
{
    public class Game : IModifiable
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

        public virtual Player LightPlayer { get; set; }
        public virtual Player DarkPlayer { get; set; }
        public virtual Player WinnerPlayer { get; set; }
        public virtual Challenge Challenge { get; set; }
    }
}