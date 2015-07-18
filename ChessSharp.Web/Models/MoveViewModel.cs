namespace ChessSharp.Web.Models
{
    public class MoveViewModel
    {
        public int StartColumn { get; set; }
        public int StartRow { get; set; }
        public int EndColumn { get; set; }
        public int EndRow { get; set; }
    }
}