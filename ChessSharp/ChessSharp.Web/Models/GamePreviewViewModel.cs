namespace ChessSharp.Web.Models
{
    public class GamePreviewViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string CurrentPlayerId { get; set; }

        public string DarkPlayerName { get; set; }
        public string LightPlayerName { get; set; }
        public string OpponentName { get; set; }
        public bool IsPlayersTurn { get; set; }
    }
}