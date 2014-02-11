using System.Collections.Generic;
using Chess.Data.Entities;

namespace ChessSharp.Web.Models
{
    public class HistoricalGamesViewModel
    {
        public List<CompletedGameViewModel> Games { get; set; }

        public HistoricalGamesViewModel()
        {
            Games = new List<CompletedGameViewModel>();
        }
    }
}