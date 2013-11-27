namespace Chess.Data.Entities
{
    public class Game : IModifiable
    {
        public long GameId { get; set; }
        public long PlayerLightId { get; set; }
        public long PlayerDarkId { get; set; }
        public int LightScore { get; set; }
        public int DarkScore { get; set; }
        public int MoveCount { get; set; }
        public string MoveHistory { get; set; }

        public virtual Square[] Squares { get; set; }
        public virtual Player PlayerLight { get; set; }
        public virtual Player PlayerDark { get; set; }
    }
}