using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Data.Enum;
using WebGrease.Css.Extensions;

namespace ChessSharp.Web.Models
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
        public ICollection<Move> Moves { get; private set; }

        public GameModel()
        {
            Board = new Board();
            LightScore = 0;
            DarkScore = 0;
            MoveCount = 0;
            Moves = new List<Move>();
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
            var defender = Board.Squares[move.EndRow][move.EndColumn].ChessPiece;
            var currentTeam = TeamToMove();

            if (piece.Team != currentTeam)
                return false;
            if (!piece.IsLegalMove(Board.Squares, move))
                return false;

            if (defender == null && piece.PieceType == PieceType.Pawn && Math.Abs(move.ColumnChange) == 1)
                PerformEnPassant(move);
            if (piece.PieceType == PieceType.King && Math.Abs(move.ColumnChange) > 1)
                MoveRookForCastle(move);

            piece.Move(Board.Squares, move);
            MoveCount++;

            return IsKingInCheck(currentTeam);
        }

        private bool IsKingInCheck(Team currentTeam)
        {
            var enemy = Team.Light;
            if (currentTeam == Team.Light) enemy = Team.Dark;

            bool kingIsSafe = true;
            foreach (var row in Board.Squares)
                row.Where(sq => sq.ChessPiece != null
                                && sq.ChessPiece.PieceType == PieceType.King
                                && sq.ChessPiece.Team == currentTeam)
                    .ForEach(sq =>
                    {
                        if (sq.TargetedByTeam(Board.Squares, enemy))
                            kingIsSafe = false;
                    });
            return kingIsSafe;
        }

        private void MoveRookForCastle(Move move)
        {
            var direction = move.ColumnChange > 0 ? 1 : -1;

            var rook = direction > 0
                ? Board.Squares[move.EndRow][7].ChessPiece
                : Board.Squares[move.EndRow][0].ChessPiece;

            var rookMove = new Move()
            {
                EndColumn = move.EndColumn - direction,
                EndRow = move.EndRow,
                StartColumn = rook.CurrentColumn ?? 0,
                StartRow = move.StartRow,
            };

            rook.Move(Board.Squares, rookMove);
        }

        private void PerformEnPassant(Move move)
        {
            var direction = move.RowChange > 0 ? 1 : -1;
            var enemySquare = Board.Squares[move.EndRow - direction][move.EndColumn];

            enemySquare.ChessPiece.Alive = false;
            enemySquare.ChessPiece = null;
        }
    }
}