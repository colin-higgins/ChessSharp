using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessSharp.Web.Models
{
    public class CreateChallengeViewModel : ChallengeViewModel
    {
        [Required]
        [Display(Name = "Opponent")]
        public string OpponentId { get; set; }
        [Display(Name = "Check the Box for Dark")]
        public bool IsPlayerDark { get; set; }

        public List<PlayerViewModel> Players { get; set; }
        public List<ChallengeViewModel> CurrentChallenges { get; set; } 
    }
}