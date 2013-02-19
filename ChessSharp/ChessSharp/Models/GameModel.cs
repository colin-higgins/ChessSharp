using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using SharpCentral;

namespace ChessSharp.Models
{
    public class GameModel
    {
        public int id { get; private set; }
        public ChessItems.Board board { get; private set; }
        public int playerLightIdent { get; private set; }
        public int playerDarkIdent { get; private set; }
        public int lightScore { get; private set; }
        public int darkScore { get; private set; }
        public int moveCount { get; private set; }
        public string moveHistory { get; private set; }
        public int? EnPassantOpportunityIndex { get; private set; }

        /// <summary>
        /// Use this method to start a new game. 
        /// </summary>
        /// <param name="playerLight"></param>
        /// <param name="playerDark"></param>
        public GameModel(int playerLight, int playerDark)
        {
            id = -1;
            board = new ChessItems.Board();
            playerLightIdent = playerLight;
            playerDarkIdent = playerDark;
            lightScore = 0;
            darkScore = 0;
            moveCount = 0;
            moveHistory = "";
        }

        public bool MovePiece(int currentPosition, int newPosition)
        {
            bool success = false;

            if (board.MovePiece(currentPosition, newPosition, moveCount))
            {
                success = true;
                moveCount++;
                EnPassantOpportunityIndex = null;
                if (board.getSquare(newPosition).getOccupant().PieceType == Piece.dPawn || board.getSquare(newPosition).getOccupant().PieceType == Piece.lPawn)
                {
                    if (Math.Abs(currentPosition - newPosition) == 2)
                    {
                        EnPassantOpportunityIndex = newPosition;
                    }
                }
            }

            return success;
        }
    }




    public class GameDBContext : DbContext
    {
        public ChessSharpEntities db { get; set; }

        public bool SaveNewGame(GameModel game)
        {
            bool success = false;

            Game freshGame = new Game();
            var sqBox = game.board.pieceChest;


            //freshGame.Ident = game.id;
            freshGame.Light_PlayerIdent = game.playerLightIdent;
            freshGame.Dark_PlayerIdent = game.playerDarkIdent;
            freshGame.MoveHistory = "";
            freshGame.Start_Date = DateTime.Now; //This may not start until both players accept?
            freshGame.GameName = "First Game Ever";
            freshGame.Complete_Date = null;
            freshGame.Add_Date = DateTime.Now;
            freshGame.Add_PlayerIdent = 1;
            freshGame.CompleteTypeIdent = null;

            db.AddToGames(freshGame);
            db.SaveChanges();

            GamePiece[] freshChest = new GamePiece[32];

            for (var i = 0; i < 32; i++)
            {
                freshChest[i] = new GamePiece()
                {
                    Alive = true,
                    GameIdent = freshGame.Ident,
                    MoveCount = 0,
                    PieceType = (int)sqBox[i].PieceType,
                    SquareIndex = sqBox[i].currentSquare,
                    Game = freshGame
                };

                db.AddToGamePieces(freshChest[i]);
            }

            db.SaveChanges();


            return success;
        }

        public bool SaveGame(GameModel game)
        {
            bool success = false;

            Game freshGame = new Game();
            var sqBox = game.board.pieceChest;

            var sqQuery = from gp in db.GamePieces
                          where gp.GameIdent == game.id
                          select gp;
            //var sqQuery = from gp in db.GamePieces
            //             where gp.GameIdent == game.id
            //             select new 
            //             ChessItems.ChessPiece((Piece)gp.PieceType, gp.Ident, (int)gp.SquareIndex, gp.Alive, gp.MoveCount > 0 ? false : true);

            GamePiece[] freshChest = new GamePiece[32];

            for (var i = 0; i < 32; i++)
            {
                var curSq = sqQuery.Where(s => s.Ident == sqBox[i].id).Take(1);
                curSq.First().Alive = sqBox[i].alive;
                curSq.First().SquareIndex = sqBox[i].currentSquare;
            }

            db.SaveChanges();


            return success;
        }
    }
}