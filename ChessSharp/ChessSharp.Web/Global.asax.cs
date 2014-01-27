using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chess.Data;
using Chess.Data.Entities;
using Chess.Domain;
using ChessSharp.Web.Models;

namespace ChessSharp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new ChessInitializer());

            // Used to force a drop and create until development is done.
            var contextForcer = new ChessContext();
            SqlConnection.ClearAllPools();
            contextForcer.Database.Initialize(true);
            // End the forced drop and create

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperBootstrapper();
        }

        public void AutoMapperBootstrapper()
        {

            AutoMapper.Mapper.CreateMap<BoardViewModel, Board>();
            AutoMapper.Mapper.CreateMap<Board, BoardViewModel>();

            AutoMapper.Mapper.CreateMap<PlayerViewModel, Player>();
            AutoMapper.Mapper.CreateMap<Player, PlayerViewModel>();

            AutoMapper.Mapper.CreateMap<SquareViewModel, Square>();
            AutoMapper.Mapper.CreateMap<Square, SquareViewModel>();

            AutoMapper.Mapper.CreateMap<Game, GameModel>();
            AutoMapper.Mapper.CreateMap<Game, ActiveGameViewModel>();

            AutoMapper.Mapper.CreateMap<ChessPieceViewModel, ChessPiece>();
            AutoMapper.Mapper.CreateMap<ChessPiece, ChessPieceViewModel>();

            AutoMapper.Mapper.CreateMap<Challenge, ExistingChallengeViewModel>();
            AutoMapper.Mapper.CreateMap<Challenge, CreateChallengeViewModel>();
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
