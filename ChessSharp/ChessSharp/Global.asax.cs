using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chess.Data;
using Chess.Data.Entities;

namespace ChessSharp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ChessContext>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class ChessInitializer : DropCreateDatabaseIfModelChanges<ChessContext>
    {
        protected override void Seed(ChessContext context)
        {
            var spikePlayer = new Player
            { DisplayName = "SpykeBytes", UserName = "colin@spykebytes.me" };

            var allyPlayer = new Player()
            { DisplayName = "AllyDuck", UserName = "AllyDuck" };

            var baseBoard = new Board();

            var baseGame = new Game
            {
                DarkPlayer = spikePlayer,
                LightPlayer = allyPlayer,
                Squares = (ICollection<Square>) baseBoard.Squares.SelectMany(row => row.Select(square => square))
            };

            base.Seed(context);
        }
    }
}
