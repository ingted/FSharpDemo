using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FSharpDemo.Web.Startup))]
namespace FSharpDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
