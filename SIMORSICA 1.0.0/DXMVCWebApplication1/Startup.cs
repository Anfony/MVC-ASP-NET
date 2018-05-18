using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DXMVCWebApplication1.Startup))]
namespace DXMVCWebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}