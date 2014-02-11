namespace ChessSharp.Web.Models
{
    public class CompletedGameViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public int LightScore { get; set; }
        public int DarkScore { get; set; }
        public int MoveCount { get; set; }

        public string DarkPlayerName { get; set; }
        public string LightPlayerName { get; set; }
        public string WinnerPlayerName { get; set; }
        public bool? Win { get; set; }
    }
}