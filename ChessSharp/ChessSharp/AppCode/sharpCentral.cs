/* sharpCentral:
 *      This file holds all of the necessary enums and central functions to this chess application.
 *   NOTES:
 *      Created: 10/27/2012 (On the urrpluhn) by Colin Higgins
*/
namespace sharpCentral
{


    public enum pieceType
    {
        Empty = 0,
        lPawn = 1,
        lKnight = 2,
        lBishop = 3,
        lRook = 4,
        lQueen = 5,
        lKing = 6,
        dPawn = -1,
        dKnight = -2,
        dBishop = -3,
        dRook = -4,
        dQueen = -5,
        dKing = -6
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