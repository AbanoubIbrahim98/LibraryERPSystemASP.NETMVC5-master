using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryMVCAPP.Startup))]
namespace LibraryMVCAPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
