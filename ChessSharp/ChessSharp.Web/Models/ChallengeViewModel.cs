using System.Collections.Generic;

namespace ChessSharp.Web.Models
{
    public class ChallengeViewModel
    {
        public long ChallengeId { get; set; }
        public long PlayerId { get; set; }
        public long OpponentId { get; set; }
        public bool IsPlayerDark { get; set; }

        public string Name { get; set; }

        public List<PlayerViewModel> Players { get; set; } 
    }
}