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
        Piece.dRook, Piece.dKnight, Piece.dBishop, Piece.dQueen, Piece.dKing, Piece.dBishop, Piece.dKnight, Piece.dRook,
        Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, Piece.dPawn, 
        Piece.Empty ,Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, Piece.Empty, 
        Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, Piece.lPawn, 
        Piece.lRook, Piece.lKnight, Piece.lBishop, Piece.lQueen, Piece.lKing, Piece.lBishop, Piece.lKnight, Piece.lRook
        };
    }

    public class Seeker
    {
        public string GetImagePath(Piece PieceType)
        {
            switch (PieceType)
            {
                case Piece.dPawn:
                    return "~/Images/darkPawn.png";
                case Piece.dKnight:
                    return "~/Images/darkKnight.png";
                case Piece.dBishop:
                    return "~/Images/darkBishop.png"; ;
                case Piece.dRook:
                    return "~/Images/darkRook.png"; ;
                case Piece.dQueen:
                    return "~/Images/darkQueen.png";
                case Piece.dKing:
                    return "~/Images/darkKing.png";
                case Piece.lPawn:
                    return "~/Images/lightPawn.png";
                case Piece.lKnight:
                    return "~/Images/lightKnight.png";
                case Piece.lBishop:
                    return "~/Images/lightBishop.png"; ;
                case Piece.lRook:
                    return "~/Images/lightRook.png"; ;
                case Piece.lQueen:
                    return "~/Images/lightQueen.png";
                case Piece.lKing:
                    return "~/Images/lightKing.png";
                default:
                    return "";
            }

        } //end getImagePath

        public string GetName(Piece PieceType)
        {
            switch (PieceType)
            {
                case Piece.dPawn:
                    return "darkPawn";
                case Piece.dKnight:
                    return "darkKnight";
                case Piece.dBishop:
                    return "darkBishop"; ;
                case Piece.dRook:
                    return "darkRook"; ;
                case Piece.dQueen:
                    return "darkQueen";
                case Piece.dKing:
                    return "darkKing";
                case Piece.lPawn:
                    return "lightPawn";
                case Piece.lKnight:
                    return "lightKnight";
                case Piece.lBishop:
                    return "lightBishop";
                case Piece.lRook:
                    return "lightRook";
                case Piece.lQueen:
                    return "lightQueen";
                case Piece.lKing:
                    return "lightKing";
                default:
                    return "Empty";
            }

        } //end getName
    }
}