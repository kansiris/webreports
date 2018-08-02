using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(report_ankapur.Startup))]
namespace report_ankapur
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
