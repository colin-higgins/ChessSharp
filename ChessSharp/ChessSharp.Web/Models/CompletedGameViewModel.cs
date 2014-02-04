namespace ChessSharp.Web.Models
{
    public class CompletedGameViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DarkPlayerName { get; set; }
        public string LightPlayerName { get; set; }
        public bool? Win { get; set; }
    }
}