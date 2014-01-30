namespace ChessSharp.Web.Models
{
    public class PlayerViewModel
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}