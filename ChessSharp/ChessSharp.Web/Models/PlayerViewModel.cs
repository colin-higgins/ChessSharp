namespace ChessSharp.Web.Models
{
    public class PlayerViewModel
    {
        public string PlayerId { get; set; }
        public string Name { get; set; }

        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}