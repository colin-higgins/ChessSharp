﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using sharpCentral;

namespace ChessSharp.Models
{


    public class TempGameModel
    {
        public sharpCentral.pieceType[] board { get; set; }
        public int playerLightIdent { get; set; }
        public int playerDarkIdent { get; set; }
        public int lightScore { get; set; }
        public int darkScore { get; set; }
        public string moveHistory { get; set; }
    }




    public class GameDBContext : DbContext
    {
        public DbSet<TempGameModel> Games { get; set; }
    }
}   