using System;
using System.ComponentModel.DataAnnotations;

namespace Chess.Data.Entities
{
    public class Challenge : IModifiable
    {
        [Key]
        public long ChallengeId { get; set; }

        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public bool? Accepted { get; set; }

        public virtual ChessUser ChallengingPlayer { get; set; }
        public virtual ChessUser LightPlayer { get; set; }
        public virtual ChessUser DarkPlayer { get; set; }
    }
}