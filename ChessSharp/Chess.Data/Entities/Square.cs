namespace Chess.Data.Entities
{
    public class Square
    {
        public int SquareId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int? ChessPieceId { get; set; }
        public virtual ChessPiece ChessPiece { get; set; } //if null, there is no occupant
    }
}
