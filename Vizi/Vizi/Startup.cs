using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vizi.Startup))]
namespace Vizi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
