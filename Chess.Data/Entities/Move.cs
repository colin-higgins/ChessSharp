using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Data.Entities
{
    public class Move
    {
        [Key]
        public long MoveId { get; set; }

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

        public bool Equals(Move move)
        {
            if (StartColumn != move.StartColumn)
                return false;
            if (EndColumn != move.EndColumn)
                return false;
            if (StartRow != move.StartRow)
                return false;
            if (EndRow != move.EndRow)
                return false;
            return true;
        }
    }
}