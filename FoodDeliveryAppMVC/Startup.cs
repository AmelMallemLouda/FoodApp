using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodDeliveryAppMVC.Startup))]
namespace FoodDeliveryAppMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
