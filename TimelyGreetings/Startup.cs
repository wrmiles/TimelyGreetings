using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimelyGreetings.Startup))]
namespace TimelyGreetings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
