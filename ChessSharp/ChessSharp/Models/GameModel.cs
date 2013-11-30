using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data;
using Chess.Data.Enum;
using WebGrease.Css.Extensions;

namespace ChessSharp.Models
{
    public class GameModel
    {
        public long GameId { get; set; }
        public Board Board { get; set; }
        public int PlayerLightId { get; set; }
        public int PlayerDarkId { get; set; }
        public int LightScore { get; private set; }
        public int DarkScore { get; private set; }
        public int MoveCount { get; private set; }
        public ICollection<Move> MoveHistory { get; private set; }

        public GameModel()
        {
            Board = new Board();
            LightScore = 0;
            DarkScore = 0;
            MoveCount = 0;
            MoveHistory = new List<Move>();
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

            if (piece.PieceType == PieceType.King && Math.Abs(move.ColumnChange) > 1)
                MoveRookForCastle(move);

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

        private void MoveRookForCastle(Move move)
        {
            var direction = move.ColumnChange > 0 ? 1 : -1;
            var rook = Board.Squares[move.EndRow][move.EndColumn + direction].ChessPiece;
            var rookMove = new Move()
            {
                EndColumn = move.EndColumn - direction,
                EndRow = move.EndRow,
                StartColumn = rook.CurrentColumn ?? 0,
                StartRow = rook.CurrentRow ?? 0,
            };

            rook.Move(Board.Squares, rookMove);
        }
    }
}