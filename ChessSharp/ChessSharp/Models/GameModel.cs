using System;
using System.Linq;
using Chess.Data;
using Chess.Data.Enum;
using WebGrease.Css.Extensions;

namespace ChessSharp.Models
{
    [Serializable]
    public class GameModel
    {
        public long GameId { get; set; }
        public Board Board { get; set; }
        public int PlayerLightId { get; set; }
        public int PlayerDarkId { get; set; }
        public int LightScore { get; private set; }
        public int DarkScore { get; private set; }
        public int MoveCount { get; private set; }
        public string MoveHistory { get; private set; }

        public GameModel()
        {
            Board = new Board();
            LightScore = 0;
            DarkScore = 0;
            MoveCount = 0;
            MoveHistory = "";
        }

        private Team TeamToMove()
        {
            if (MoveCount % 2 == 0)
                return Team.Light;
            return Team.Dark;
        }

        public bool MovePiece(Move move)
        {
            var piece = Board.Squares[move.StartRow][move.StartColumn].ChessPiece;
            var currentTeam = TeamToMove();

            if (piece.Team != currentTeam)
                return false;
            if (!piece.IsLegalMove(Board.Squares, move))
                return false;

            piece.Move(Board.Squares, move);

            var nextTeamToMove = TeamToMove();
            var kingIsSafe = true;

            foreach (var row in Board.Squares)
                row.Where(sq => sq.ChessPiece != null 
                                && sq.ChessPiece.PieceType == PieceType.King 
                                && sq.ChessPiece.Team == currentTeam)
                    .ForEach(sq =>
                    {
                        if (sq.TargetedByTeam(Board.Squares, nextTeamToMove))
                            kingIsSafe = false;
                    });

            return kingIsSafe;
        }
    }
}