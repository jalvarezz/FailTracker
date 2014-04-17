using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FailTracker.Startup))]
namespace FailTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
