using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SLD521SA.Startup))]
namespace SLD521SA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
