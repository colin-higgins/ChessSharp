using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChessSharp.Web.Startup))]
namespace ChessSharp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
