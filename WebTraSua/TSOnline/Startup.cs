using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TSOnline.Startup))]
namespace TSOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
