﻿using System;
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

        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public bool Accepted { get; set; }

        public virtual Player ChallengingPlayer { get; set; }
        public virtual Player LightPlayer { get; set; }
        public virtual Player DarkPlayer { get; set; }
    }
}