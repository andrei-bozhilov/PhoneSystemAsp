using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhoneSystem.Web.Startup))]
namespace PhoneSystem.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
