namespace Chess.Data.Entities
{
    public class Player : IModifiable
    {
        public long PlayerId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public long Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}
