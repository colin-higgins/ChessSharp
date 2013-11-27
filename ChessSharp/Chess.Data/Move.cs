namespace Chess.Data
{
    public class Move
    {
        public int StartColumn { get; set; }
        public int StartRow { get; set; }
        public int EndColumn { get; set; }
        public int EndRow { get; set; }

        public int RowChange
        {
            get { return EndRow - StartRow; }
        }

        public int ColumnChange
        {
            get { return EndColumn - StartColumn; }
        }
    }
}