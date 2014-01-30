using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Chess.Data.Entities
{
    public class Square : IModifiable
    {
        [Key]
        public long SquareId { get; set; }

        public int Column { get; set; }
        public int Row { get; set; }

        public virtual Game Game { get; set; }
        public virtual ChessPiece ChessPiece { get; set; }

        public bool TargetedByTeam(Square[][] board, Enum.Team team)
        {
            foreach (var row in board)
                foreach (var square in row.Where(square => square.ChessPiece != null && square.ChessPiece.Team == team))
                {
                    var possibleMove = new Move
                    {
                        EndColumn = Column,
                        EndRow = Row,
                        StartColumn = square.Column,
                        StartRow = square.Row
                    };

                    if (square.ChessPiece.IsLegalMove(board, possibleMove))
                        return true;
                }
            return false;
        }
    }
}
