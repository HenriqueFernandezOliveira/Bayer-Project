using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bayer.Ui.Mvc.Startup))]
namespace Bayer.Ui.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
