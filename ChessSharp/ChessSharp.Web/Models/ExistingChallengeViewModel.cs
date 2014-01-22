using System;
using Chess.Data.Entities;
using Chess.Data.Enum;

namespace ChessSharp.Web.Models
{
    public class ExistingChallengeViewModel
    {
        public long Id { get; set; }
        public string ChallengeTitle { get; set; }
        public Player Opponent { get; set; }
        public Team OpponentTeam { get; set; }
        public DateTime DateTime { get; set; }
        public bool Accepted { get; set; }
    }
}