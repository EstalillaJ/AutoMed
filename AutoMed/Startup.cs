using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutoMed.Startup))]
namespace AutoMed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
