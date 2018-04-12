using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC2015.Web.Site.Startup))]
namespace MVC2015.Web.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
