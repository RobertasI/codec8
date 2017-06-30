using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Map.Startup))]
namespace Map
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
