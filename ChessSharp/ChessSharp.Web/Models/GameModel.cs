using System.Collections.Generic;
using Chess.Data.Entities;

namespace ChessSharp.Web.Models
{
    public class GameModel
    {
        public long Id { get; set; }
        public BoardViewModel Board { get; set; }
        public PlayerViewModel PlayerLight { get; set; }
        public PlayerViewModel PlayerDark { get; set; }
        public PlayerViewModel Winner { get; set; }

        public string Name { get; set; }
        public int LightScore { get; set; }
        public int DarkScore { get; set; }
        public int MoveCount { get; set; }
        public bool IsCurrentPlayersMove { get; set; }
        public bool Complete { get; set; }

        public List<Move> Moves { get; set; }

        public GameModel()
        {
            Board = new BoardViewModel();
            Moves = new List<Move>();
        }
    }
}