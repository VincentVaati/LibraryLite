using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryLite.UI.Web.MVC.Startup))]
namespace LibraryLite.UI.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
