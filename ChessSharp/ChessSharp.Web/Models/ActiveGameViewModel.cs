namespace ChessSharp.Web.Models
{
    public class ActiveGameViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CurrentPlayerId { get; set; }

        public string DarkPlayerName { get; set; }
        public string LightPlayerName { get; set; }
    }
}