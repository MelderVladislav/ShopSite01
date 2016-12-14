using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopSite.Startup))]
namespace ShopSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
