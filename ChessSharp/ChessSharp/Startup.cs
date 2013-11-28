using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChessSharp.Startup))]
namespace ChessSharp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
