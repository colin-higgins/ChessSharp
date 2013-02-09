/* sharpCentral:
 *      This file holds all of the necessary enums and central functions to this chess application.
 *   NOTES:
 *      Created: 10/27/2012 (On the urrpluhn) by Colin Higgins
*/
namespace SharpCentral
{
    
    public enum team
    {
        light,
        dark
    };

    public enum Piece
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

    class FreshGame
    {
        public Piece[] chessBoard = new Piece[64] 
        {
        Piece.dRook, Piece.dKnight, Piece.dBishop, Piece.dKing, Piece.dQueen, Piece.dBishop, Piece.dKnight, Piece.dRook,
        Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, 
        Piece.Empty ,Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, 
        Piece.lRook, Piece.lKnight, Piece.lBishop, Piece.lKing, Piece.lQueen, Piece.lBishop, Piece.lKnight, Piece.lRook
        };
    }
}