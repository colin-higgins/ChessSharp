using System.Linq;

namespace Chess.Data.Entities
{
    public class Square
    {
        public int SquareId { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int? ChessPieceId { get; set; }
        public virtual ChessPiece ChessPiece { get; set; } //if null, there is no occupant

        public bool TargetedByTeam(Square[][] board, Enum.Team team)
        {
            var possibleMove = new Move() { EndColumn = Column, EndRow = Row };

            foreach (var row in board)
                foreach (var square in row.Where(square => square.ChessPiece != null))
                {
                    possibleMove.EndColumn = square.Column;
                    possibleMove.EndRow = square.Row;
                    if (square.ChessPiece.IsLegalMove(board, possibleMove))
                        return true;
                }
            return false;
        }
    }
}
