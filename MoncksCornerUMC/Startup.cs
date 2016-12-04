using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoncksCornerUMC.Startup))]
namespace MoncksCornerUMC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
