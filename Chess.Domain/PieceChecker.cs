using Chess.Data.Entities;
using Chess.Data.Enum;
using Chess.Data.Piece;

namespace Chess.Domain
{
    public static class PieceChecker
    {
        public static PieceType GetPieceType(ChessPiece piece)
        {
            var pieceType = piece.GetType();

            if (typeof(Pawn) == pieceType)
                return PieceType.Pawn;
            
            if (typeof(Knight) == pieceType)
                return PieceType.Knight;
            
            if (typeof(Bishop) == pieceType)
                return PieceType.Bishop;
            
            if (typeof(Rook) == pieceType)
                return PieceType.Rook;
            
            if (typeof(Queen) == pieceType)
                return PieceType.Queen;
        
            if (typeof(King) == pieceType)
                return PieceType.King;

            return PieceType.Empty;
        }
    }
}
