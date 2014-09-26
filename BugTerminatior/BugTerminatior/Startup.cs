using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugTerminatior.Startup))]
namespace BugTerminatior
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
