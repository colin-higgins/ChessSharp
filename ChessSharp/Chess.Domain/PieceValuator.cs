using Chess.Data.Enum;

namespace Chess.Domain
{
    public static class PieceValuator
    {
        public static int GetPieceScoreValue(PieceType type)
        {
            switch (type)
            {
                case PieceType.Pawn:
                    return 100;
                case PieceType.Knight:
                    return 320;
                case PieceType.Bishop:
                    return 325;
                case PieceType.Rook:
                    return 500;
                case PieceType.Queen:
                    return 1000;
                case PieceType.King:
                    return 32767;
                default:
                    return 0;
            }
        }
        public static int GetPieceActionValue(PieceType type)
        {
            switch (type)
            {
                case PieceType.Pawn:
                    return 10;
                case PieceType.Knight:
                    return 8;
                case PieceType.Bishop:
                    return 8;
                case PieceType.Rook:
                    return 6;
                case PieceType.Queen:
                    return 3;
                case PieceType.King:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
