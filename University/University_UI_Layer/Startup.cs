using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(University_UI_Layer.Startup))]
namespace University_UI_Layer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
