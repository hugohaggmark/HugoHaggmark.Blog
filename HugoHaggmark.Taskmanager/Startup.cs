using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HugoHaggmark.Taskmanager.Startup))]

namespace HugoHaggmark.Taskmanager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .MapSignalR()
                .UseNancy();
        }
    }
}
