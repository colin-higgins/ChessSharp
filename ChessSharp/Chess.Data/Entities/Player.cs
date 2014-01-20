using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Data.Entities
{
    public class Player : IModifiable
    {
        [Key]
        public long PlayerId { get; set; }

        public string ChessUserId { get; set; }

        [MaxLength(30)]
        public string DisplayName { get; set; }

        public int Rank { get; set; }
        public int Games { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        public virtual ChessUser ChessUser { get; set; }
    }
}
