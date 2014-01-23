﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Chess.Data.Entities
{
    public class Challenge : IModifiable
    {
        [Key]
        public long ChallengeId { get; set; }
        public long ChallengingPlayerId { get; set; }
        public long LightPlayerId { get; set; }
        public long DarkPlayerId { get; set; }

        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public bool? Accepted { get; set; }

        //[ForeignKey("ChallengingPlayerId")]
        public virtual Player ChallengingPlayer { get; set; }
        //[ForeignKey("LightPlayerId")]
        public virtual Player LightPlayer { get; set; }
        //[ForeignKey("DarkPlayerId")]
        public virtual Player DarkPlayer { get; set; }
    }
}