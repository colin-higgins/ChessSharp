using System;
using Chess.Data;

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

        public bool MovePiece(Move move)
        {
            var piece = Board.Squares[move.StartRow][move.StartColumn].ChessPiece;

            if (!piece.IsLegalMove(move.EndColumn, move.EndRow))
                return false;

            piece.Move(move.EndColumn, move.EndRow);

            return true;
        }
    }
}