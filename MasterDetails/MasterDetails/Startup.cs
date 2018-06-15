using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MasterDetails.Startup))]
namespace MasterDetails
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
