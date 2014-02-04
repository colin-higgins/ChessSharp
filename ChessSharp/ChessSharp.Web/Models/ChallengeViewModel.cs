using System.ComponentModel.DataAnnotations;

namespace ChessSharp.Web.Models
{
    public class ChallengeViewModel
    {
        public long Id { get; set; }
        
        [Display(Name = "Challenger")]
        public string ChallengerId { get; set; }
        
        [Display(Name = "Light Player")]
        public string LightPlayerId { get; set; }
        
        [Display(Name = "Dark Player")]
        public string DarkPlayerId { get; set; }
        
        [Display(Name = "Challenge Title")]
        public string ChallengeTitle { get; set; }

        public bool Accepted { get; set; }

        public PlayerViewModel Challenger { get; set; }
    }
}