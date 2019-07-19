using Autofac.Integration.WebApi;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute("http://localhost:53017", "*", "*");
            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);// Web API configuration and services  
            
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            // Configure Web API to use only bearer token authentication.  
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes  
            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new
            {
                id = RouteParameter.Optional
            });
        }
    }
}