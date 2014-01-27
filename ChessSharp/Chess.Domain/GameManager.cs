using System;
using System.Linq;
using Chess.Data.Entities;
using Chess.Data.Enum;

namespace Chess.Domain
{
    public class GameManager
    {
        public Game Game { get; set; }
        private readonly Board _board;

        public GameManager(Game game)
        {
            Game = game;
            var squares = game.Squares.Select(s => s).ToArray();
            _board = new Board(squares);
        }

        private Team TeamToMove()
        {
            return Game.MoveCount % 2 == 0 ? Team.Light : Team.Dark;
        }

        public bool MovePiece(Move move)
        {
            var piece = _board.Squares[move.StartRow][move.StartColumn].ChessPiece;
            var defender = _board.Squares[move.EndRow][move.EndColumn].ChessPiece;
            var currentTeam = TeamToMove();

            if (piece.Team != currentTeam)
                return false;
            if (!piece.IsLegalMove(_board.Squares, move))
                return false;

            if (defender == null && piece.PieceType == PieceType.Pawn && Math.Abs(move.ColumnChange) == 1)
                PerformEnPassant(move);
            if (piece.PieceType == PieceType.King && Math.Abs(move.ColumnChange) > 1)
                MoveRookForCastle(move);

            piece.Move(_board.Squares, move);
            Game.MoveCount++;

            return IsKingInCheck(currentTeam);
        }

        private bool IsKingInCheck(Team currentTeam)
        {
            var enemy = Team.Light;
            if (currentTeam == Team.Light) enemy = Team.Dark;

            var squares = _board.Squares.SelectMany(s => s);

            var kingSquare = squares.First(sq => sq.ChessPiece != null
                                           && sq.ChessPiece.PieceType == PieceType.King
                                           && sq.ChessPiece.Team == currentTeam);

            if (kingSquare.TargetedByTeam(_board.Squares, enemy))
                return true;

            return false;
        }

        private void MoveRookForCastle(Move move)
        {
            var direction = move.ColumnChange > 0 ? 1 : -1;

            var rook = direction > 0
                ? _board.Squares[move.EndRow][7].ChessPiece
                : _board.Squares[move.EndRow][0].ChessPiece;

            var rookMove = new Move()
            {
                EndColumn = move.EndColumn - direction,
                EndRow = move.EndRow,
                StartColumn = rook.CurrentColumn ?? 0,
                StartRow = move.StartRow,
            };

            rook.Move(_board.Squares, rookMove);
        }

        private void PerformEnPassant(Move move)
        {
            var direction = move.RowChange > 0 ? 1 : -1;
            var enemySquare = _board.Squares[move.EndRow - direction][move.EndColumn];

            enemySquare.ChessPiece.Alive = false;
            enemySquare.ChessPiece = null;
        }
    }
}
