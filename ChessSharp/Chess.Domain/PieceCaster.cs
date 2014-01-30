using Chess.Data.Entities;
using Chess.Data.Enum;
using Chess.Data.Piece;

namespace Chess.Domain
{
    public class PieceCaster
    {
        private class PieceMapper<T> where T : ChessPiece, new()
        {
            public T MapPiece(ChessPiece piece)
            {
                return new T()
                {
                    GameId = piece.GameId,
                    Alive = piece.Alive,
                    ChessPieceId = piece.ChessPieceId,
                    CurrentColumn = piece.CurrentColumn,
                    CurrentRow = piece.CurrentRow,
                    ActionValue = piece.ActionValue,
                    AttackValue = piece.AttackValue,
                    DefenseValue = piece.DefenseValue,
                    MoveCount = piece.MoveCount,
                    PieceType = piece.PieceType,
                    Team = piece.Team
                };
            }
        }

        public ChessPiece MapPiece(ChessPiece piece)
        {
            switch (piece.PieceType)
            {
                case PieceType.Pawn:
                    return new PieceMapper<Pawn>().MapPiece(piece);
                case PieceType.Knight:
                    return new PieceMapper<Knight>().MapPiece(piece);
                case PieceType.Bishop:
                    return new PieceMapper<Bishop>().MapPiece(piece);
                case PieceType.Rook:
                    return new PieceMapper<Rook>().MapPiece(piece);
                case PieceType.Queen:
                    return new PieceMapper<Queen>().MapPiece(piece);
                case PieceType.King:
                    return new PieceMapper<King>().MapPiece(piece);
            }

            return piece;
        }
    }
}