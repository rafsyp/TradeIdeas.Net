using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TradeIdeasNet.Startup))]
namespace TradeIdeasNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
