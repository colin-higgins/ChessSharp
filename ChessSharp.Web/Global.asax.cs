using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Data.Enum;
using Chess.Data.Piece;
using Chess.Domain;
using ChessSharp.Web.Models;

namespace ChessSharp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new ChessInitializer());

            //// Used to force a drop and create until development is done.
            //var contextForcer = new ChessContext();
            //SqlConnection.ClearAllPools();
            //contextForcer.Database.Initialize(true);
            // End the forced drop and create

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperBootstrapper();
        }

        public void AutoMapperBootstrapper()
        {
            MapMoveTypes();

            MapChessPieceTypes();

            MapPlayerTypes();

            MapSquareTypes();

            MapGameTypes();

            MapChallengeTypes();

            MapBoardTypes();
        }

        private static void MapPlayerTypes()
        {
            AutoMapper.Mapper.CreateMap<ChessUser, PlayerViewModel>();
            AutoMapper.Mapper.CreateMap<PlayerViewModel, ChessUser>();
        }

        private static void MapSquareTypes()
        {
            AutoMapper.Mapper.CreateMap<Square, SquareViewModel>();

            System.Func<ChessPieceViewModel, ChessPiece> mapFromVm =
                (vm) =>
                {
                    if (vm == null)
                    {
                        return null;
                    }

                    switch (vm.PieceType)
                    {
                        case PieceType.Pawn:
                            return Map<Pawn>(vm);
                        case PieceType.Knight:
                            return Map<Knight>(vm);
                        case PieceType.Bishop:
                            return Map<Bishop>(vm);
                        case PieceType.Rook:
                            return Map<Rook>(vm);
                        case PieceType.Queen:
                            return Map<Queen>(vm);
                        case PieceType.King:
                            return Map<King>(vm);
                    }

                    return null;
                };

            AutoMapper.Mapper.CreateMap<SquareViewModel, Square>()
                .ForMember(dest => dest.ChessPiece, opt => opt.MapFrom(src => mapFromVm(src.ChessPiece)));
        }

        private static void MapChallengeTypes()
        {
            AutoMapper.Mapper.CreateMap<Challenge, ExistingChallengeViewModel>();
            AutoMapper.Mapper.CreateMap<Challenge, CreateChallengeViewModel>();
        }

        private static void MapBoardTypes()
        {
            AutoMapper.Mapper.CreateMap<Board, BoardViewModel>();
            AutoMapper.Mapper.CreateMap<BoardViewModel, BoardViewModel>();
        }

        private static void MapGameTypes()
        {
            AutoMapper.Mapper.CreateMap<Game, GameModel>();
            AutoMapper.Mapper.CreateMap<GameModel, Game>()
                .ForMember(dest => dest.Squares, opt => opt.MapFrom(src => src.Board.Squares.SelectMany(s => s)));

            AutoMapper.Mapper.CreateMap<Game, GamePreviewViewModel>()
                .ForMember(dest => dest.DarkPlayerName, opt => opt.MapFrom(src => src.DarkPlayer.DisplayName))
                .ForMember(dest => dest.LightPlayerName, opt => opt.MapFrom(src => src.LightPlayer.DisplayName));

            AutoMapper.Mapper.CreateMap<Game, CompletedGameViewModel>()
                .ForMember(dest => dest.DarkPlayerName, opt => opt.MapFrom(src => src.DarkPlayer.DisplayName))
                .ForMember(dest => dest.LightPlayerName, opt => opt.MapFrom(src => src.LightPlayer.DisplayName));
        }

        private static void MapChessPieceTypes()
        {
            AutoMapper.Mapper.CreateMap<Rook, ChessPieceViewModel>();
            AutoMapper.Mapper.CreateMap<Bishop, ChessPieceViewModel>();
            AutoMapper.Mapper.CreateMap<Knight, ChessPieceViewModel>();
            AutoMapper.Mapper.CreateMap<Queen, ChessPieceViewModel>();
            AutoMapper.Mapper.CreateMap<King, ChessPieceViewModel>();
            AutoMapper.Mapper.CreateMap<Pawn, ChessPieceViewModel>();
            AutoMapper.Mapper.CreateMap<ChessPiece, ChessPieceViewModel>();

            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, Rook>();
            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, Bishop>();
            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, Knight>();
            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, Queen>();
            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, King>();
            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, Pawn>();
            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, ChessPiece>();
        }

        private static void MapMoveTypes()
        {
            AutoMapper.Mapper.CreateMap<Move, MoveViewModel>();
            AutoMapper.Mapper.CreateMap<MoveViewModel, Move>();
        }

        private static TDest Map<TDest>(object source)
        {
            return AutoMapper.Mapper.Map<TDest>(source);
        }

        class ChessInitializer : DropCreateDatabaseIfModelChanges<ChessContext>
        {
            protected override void Seed(ChessContext context)
            {
                base.Seed(context);
            }
        }
    }
}
