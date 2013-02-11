using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using SharpCentral;

namespace ChessSharp.Models
{
    public class GameModel
    {
        public long id { get; private set; }
        public ChessItems.Board board { get; private set; }
        public int playerLightIdent { get; private set; }
        public int playerDarkIdent { get; private set; }
        public int lightScore { get; private set; }
        public int darkScore { get; private set; }
        public int moveCount { get; private set; }
        public string moveHistory { get; private set; }

        /// <summary>
        /// Use this method to start a new game. 
        /// </summary>
        /// <param name="playerLight"></param>
        /// <param name="playerDark"></param>
        public GameModel(int playerLight, int playerDark) 
        {
            id = DateTime.Now.Ticks;
            board = new ChessItems.Board();
            playerLightIdent = playerLight;
            playerDarkIdent = playerDark;
            lightScore = 0;
            darkScore = 0;
            moveCount = 0;
            moveHistory = "";
        }

        public bool MovePiece(int currentPosition, int newPosition)
        {
            bool success = false;

            if ( board.MovePiece(currentPosition, newPosition) )
                success = true;

            return success;
        }
    }




    public class GameDBContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }
    }
}   