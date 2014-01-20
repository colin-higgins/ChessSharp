using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessSharp.Web.Models
{
    public class CreateChallengeViewModel : ChallengeViewModel
    {
        [Display(Name = "Opponent")]
        public long OpponentId { get; set; }
        [Display(Name = "Check the Box to be Dark")]
        public bool IsPlayerDark { get; set; }

        public List<PlayerViewModel> Players { get; set; }
        public List<ChallengeViewModel> CurrentChallenges { get; set; } 
    }
}