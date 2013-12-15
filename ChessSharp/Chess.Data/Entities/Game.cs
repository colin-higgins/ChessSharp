using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Data.Entities
{
    public class Game : IModifiable
    {
        [Key]
        public long GameId { get; set; }

        [ForeignKey("Player")]
        public long LightPlayerId { get; set; }
        [ForeignKey("Player")]
        public long DarkPlayerId { get; set; }
        [ForeignKey("Player")]
        public long? WinnerPlayerId { get; set; }

        public string Name { get; set; }
        public int LightScore { get; set; }
        public int DarkScore { get; set; }
        public int MoveCount { get; set; }
        public bool Complete { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
        public virtual ICollection<Square> Squares { get; set; }
        public virtual Player LightPlayer { get; set; }
        public virtual Player DarkPlayer { get; set; }
        public virtual Player WinnerPlayer { get; set; }
    }
}