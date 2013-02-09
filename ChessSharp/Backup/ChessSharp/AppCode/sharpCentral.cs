/* sharpCentral:
 *      This file holds all of the necessary enums and central functions to this chess application.
 *   NOTES:
 *      Created: 10/27/2012 (On the urrpluhn) by Colin Higgins
*/
namespace sharpCentral
{

    public enum pieceName
    {
        pawn = (int)1,
        knight = (int)2,
        bishop = (int)3,
        rook = (int)4,
        queen = (int)5,
        king = (int)6
    };


    public enum pieceType
    {
        pawn,
        knight,
        bishop,
        rook,
        queen,
        king
    };
    
    public enum pieceColor
    {
        white,
        black
    };

    public enum pieceType
    {
        Empty = (int)0,
        lPawn = (int)1,
        lKnight = (int)2,
        lBishop = (int)3,
        lRook = (int)4,
        lQueen = (int)5,
        lKing = (int)6,
        dPawn = (int)-1,
        dKnight = (int)-2,
        dBishop = (int)-3,
        dRook = (int)-4,
        dQueen = (int)-5,
        dKing = (int)-6
    };

    class chessGameProperties
    {


        public pieceType[] chessBoard = new pieceType[64] 
        {
        pieceType.dRook, pieceType.dKnight, pieceType.dBishop, pieceType.dKing, pieceType.dQueen, pieceType.dBishop, pieceType.dKnight, pieceType.dRook,
        pieceType.dPawn, pieceType.dPawn, pieceType.dPawn, pieceType.dPawn, pieceType.dPawn, pieceType.dPawn, pieceType.dPawn, pieceType.dPawn, 
        pieceType.Empty ,pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, 
        pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, 
        pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, 
        pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, pieceType.Empty, 
        pieceType.lPawn, pieceType.lPawn, pieceType.lPawn, pieceType.lPawn, pieceType.lPawn, pieceType.lPawn, pieceType.lPawn, pieceType.lPawn, 
        pieceType.lRook, pieceType.lKnight, pieceType.lBishop, pieceType.lKing, pieceType.lQueen, pieceType.lBishop, pieceType.lKnight, pieceType.lRook
        };
    }
}