using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Data.Entities
{
    public class Move
    {
        [Key]
        public long MoveId { get; set; }

        public long GameId { get; set; }

        public int StartColumn { get; set; }
        public int StartRow { get; set; }
        public int EndColumn { get; set; }
        public int EndRow { get; set; }

        public virtual Game Game { get; set; }

        [NotMapped]
        public int RowChange
        {
            get { return EndRow - StartRow; }
        }

        [NotMapped]
        public int ColumnChange
        {
            get { return EndColumn - StartColumn; }
        }
    }
}