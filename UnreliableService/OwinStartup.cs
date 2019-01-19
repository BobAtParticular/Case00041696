using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;

namespace UnreliableService
{
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
        }
    }
}
