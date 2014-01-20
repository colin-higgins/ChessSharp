using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chess.Data;

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
