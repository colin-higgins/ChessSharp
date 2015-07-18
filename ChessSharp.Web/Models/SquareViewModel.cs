namespace ChessSharp.Web.Models
{
    public class SquareViewModel
    {
        public long SquareId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public ChessPieceViewModel ChessPiece { get; set; }
    }
}
