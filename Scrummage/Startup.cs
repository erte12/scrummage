using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Scrummage.Startup))]
namespace Scrummage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
