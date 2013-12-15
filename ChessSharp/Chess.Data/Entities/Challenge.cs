using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Data.Entities
{
    public class Challenge : IModifiable
    {
        [Key]
        public long ChallengeId { get; set; }
        [ForeignKey("Player")]
        public long ChallengingPlayerId { get; set; }
        [ForeignKey("Player")]
        public long LightPlayerId { get; set; }
        [ForeignKey("Player")]
        public long DarkPlayerId { get; set; }

        public DateTime ChallengeDate { get; set; }
        public string Name { get; set; }

        public virtual Player ChallengingPlayer { get; set; }
        public virtual Player LightPlayer { get; set; }
        public virtual Player DarkPlayer { get; set; }
    }
}