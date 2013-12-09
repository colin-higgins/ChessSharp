using System.ComponentModel.DataAnnotations;

namespace Chess.Data.Entities
{
    public class Player : IModifiable
    {
        [Key]
        public long PlayerId { get; set; }

        [StringLength(90)]
        public string UserName { get; set; }
        [StringLength(30)]
        public string DisplayName { get; set; }

        public int Rank { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}
