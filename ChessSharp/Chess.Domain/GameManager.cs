using System;
using System.Collections.Generic;
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

        public void MarkGameAsDraw()
        {
            Game.WinnerPlayer = null;
            Game.LightPlayer.Ties++;
            Game.DarkPlayer.Ties++;
            Game.Complete = true;
        }

        public void MarkWinningTeam(Team team)
        {
            switch (team)
            {
                case Team.Dark:
                    Game.WinnerPlayer = Game.DarkPlayer;
                    Game.LightPlayer.Losses++;
                    break;
                case Team.Light:
                    Game.WinnerPlayer = Game.LightPlayer;
                    Game.DarkPlayer.Losses++;
                    break;
            }

            Game.WinnerPlayer.Wins++;
            Game.Complete = true;
        }

        public Team TeamToMove()
        {
            return Game.MoveCount % 2 == 0 ? Team.Light : Team.Dark;
        }

        public void MovePiece(Move move)
        {
            var piece = _board.Squares[move.StartRow][move.StartColumn].ChessPiece;
            var defender = _board.Squares[move.EndRow][move.EndColumn].ChessPiece;
            var currentTeam = TeamToMove();

            ValidateActiveGame();
            ValidateIsCurrentTeam(piece);
            ValidateIsLegalMove(move, piece);

            PerformMove(move, defender, piece);

            if (piece.PieceType == PieceType.Pawn || defender != null)
                Game.MoveCountSinceProgress = 0;

            if (IsKingInCheck(currentTeam, _board.Squares))
                throw new Exception("This move leaves your king in check!");
        }

        private void PerformMove(Move move, ChessPiece defender, ChessPiece piece)
        {
            if (FitsEnPassantCriteria(move, defender, piece))
                PerformEnPassant(move);
            if (FitsCastleCriteria(move, piece))
                MoveRookForCastle(move);

            piece.Move(_board.Squares, move);
            Game.MoveCount++;
            Game.MoveCountSinceProgress++;
        }

        private void ValidateActiveGame()
        {
            if (Game.Complete)
                throw new Exception("This game has already ended!");
        }

        private bool PiecesCanCheckmate(ChessPiece[] pieces)
        {
            var kings = pieces.Count(p => p.PieceType == PieceType.King);
            var queens = pieces.Count(p => p.PieceType == PieceType.Queen);
            var rooks = pieces.Count(p => p.PieceType == PieceType.Rook);
            var bishops = pieces.Count(p => p.PieceType == PieceType.Bishop);
            var knights = pieces.Count(p => p.PieceType == PieceType.Knight);
            var pawns = pieces.Count(p => p.PieceType == PieceType.Pawn);

            if (kings < 1)
                throw new Exception("Your king is gone! Please report this error to an admin.");
            if (pawns > 0 || queens > 0 || rooks > 0)
                return true;
            if (bishops > 1 || knights > 1) //Forcing a checkmate with 2 knights is near impossible
                return true;
            if (bishops > 0 && knights > 0)
                return true;

            return false;
        }

        private bool NeitherTeamCanCheckmate()
        {
            var remainingPieces = Game.Squares
                .Where(s => s.ChessPiece != null && s.ChessPiece.Alive)
                .Select(s => s.ChessPiece).ToArray();

            var darkPieces = remainingPieces.Where(p => p.Team == Team.Dark).ToArray();
            var lightPieces = remainingPieces.Where(p => p.Team == Team.Light).ToArray();

            if (PiecesCanCheckmate(darkPieces) || PiecesCanCheckmate(lightPieces))
                return false;

            return true;
        }

        public bool IsDraw()
        {
            if (Game.Moves.Count() < 6) return false;

            if (Game.MoveCountSinceProgress > 49) return true;

            if (NeitherTeamCanCheckmate()) return true;

            var lastSixMoves = Game.Moves.Reverse().Take(6).ToArray();

            if (lastSixMoves[0].Equals(lastSixMoves[5]) && lastSixMoves[1].Equals(lastSixMoves[6]))
                return true;

            return false;
        }

        public bool IsCheckmate()
        {
            if (!IsKingInCheck(TeamToMove(), _board.Squares)) return false;

            var squares = _board.Squares.SelectMany(s => s).ToList();
            var pieces = squares
                .Where(s => s.ChessPiece != null && s.ChessPiece.Team == TeamToMove())
                .Select(s => s.ChessPiece);

            var validMoves = new List<Move>();

            foreach (var chessPiece in pieces)
                validMoves.AddRange(chessPiece.GetValidMoves());

            return !validMoves.Any(SavesKing);
        }

        private bool SavesKing(Move move)
        {
            var currentTeam = TeamToMove();
            var squares = GetMockSquares(_board.Squares);

            var piece = squares[move.StartRow][move.StartColumn].ChessPiece;

            piece.Move(squares, move);

            if (IsKingInCheck(currentTeam, squares))
                return false;

            return true;
        }

        private static Square[][] GetMockSquares(IEnumerable<Square[]> board)
        {
            var mocks = new List<Square[]>();
            var pieceCaster = new PieceCaster();

            foreach (var row in board)
            {
                var mockRow = new List<Square>();
                foreach (var square in row)
                {
                    mockRow.Add(new Square()
                    {
                        ChessPiece = pieceCaster.MapPiece(square.ChessPiece),
                        Column = square.Column,
                        Row = square.Row,
                    });
                }
                mocks.Add(mockRow.ToArray());
            }

            return mocks.ToArray();
        }

        private static bool FitsCastleCriteria(Move move, ChessPiece piece)
        {
            return piece.PieceType == PieceType.King && Math.Abs(move.ColumnChange) > 1;
        }

        private static bool FitsEnPassantCriteria(Move move, ChessPiece defender, ChessPiece piece)
        {
            return defender == null && piece.PieceType == PieceType.Pawn && Math.Abs(move.ColumnChange) == 1;
        }

        private void ValidateIsLegalMove(Move move, ChessPiece piece)
        {
            var teamName = Enum.GetName(typeof(Team), piece.Team);
            var pieceName = Enum.GetName(typeof(PieceType), piece.PieceType);

            if (!piece.IsLegalMove(_board.Squares, move))
                throw new Exception("This is not a legal move for a " + teamName + " " + pieceName + ".");
        }

        private void ValidateIsCurrentTeam(ChessPiece piece)
        {
            var teamName = Enum.GetName(typeof(Team), piece.Team);

            if (piece.Team != TeamToMove())
                throw new Exception("It is not this " + teamName + "'s turn.");
        }

        private bool IsKingInCheck(Team currentTeam, IEnumerable<Square[]> board)
        {
            var enemy = GetOppositeTeam(currentTeam);

            var squares = board.SelectMany(s => s);

            var kingSquare = squares.First(sq => sq.ChessPiece != null
                                           && sq.ChessPiece.PieceType == PieceType.King
                                           && sq.ChessPiece.Team == currentTeam);

            if (kingSquare.TargetedByTeam(_board.Squares, enemy))
                return true;

            return false;
        }

        private static Team GetOppositeTeam(Team currentTeam)
        {
            var enemy = Team.Light;
            if (currentTeam == Team.Light) enemy = Team.Dark;
            return enemy;
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
