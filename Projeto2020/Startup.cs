using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projeto2020.Startup))]
namespace Projeto2020
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
