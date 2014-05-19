using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Greenflag.Startup))]
namespace Greenflag
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
