using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContentOnTheFly.Startup))]
namespace ContentOnTheFly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
