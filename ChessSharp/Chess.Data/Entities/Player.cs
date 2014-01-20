using System.ComponentModel.DataAnnotations;

namespace Chess.Data.Entities
{
    public class Player : IModifiable
    {
        [Key]
        public long PlayerId { get; set; }

        public string ChessUser_Id { get; set; }

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
