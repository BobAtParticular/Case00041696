using System;
using System.Threading.Tasks;
using System.Web.Http;
using Messages;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using NServiceBus;
using NServiceBusConfiguration;
using Owin;
using UserApplication.WebBackend;
using UserApplication.WebContracts;

static class Program
{
    static async Task Main()
    {
        Console.Title = "UserApplication.WebBackend";

        var endpointConfiguration = new EndpointConfiguration("UserApplication.WebBackend");

        endpointConfiguration.ApplyCommonConfiguration().RegisterRoutes();

        endpointConfiguration.EnableInstallers();

        using (WebApp.Start<OwinStartup>(ServerInfo.UserApplicationWebBackendBaseAddress))
        {
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            BatchController.MessageSession = endpointInstance; //this is a bit of a cheat, would normally use DI

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }

    class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            app.UseWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}
