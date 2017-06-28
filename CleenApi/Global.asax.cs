using System.Web;
using System.Web.Http;
using CleenApi.Database;

namespace CleenApi
{
  public class WebApiApplication : HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(SetupWebApi);
    }

    private static void SetupWebApi(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});

      config.Formatters.Remove(config.Formatters.XmlFormatter);

      System.Data.Entity.Database.SetInitializer(new CleenApiDbInitializer());
    }
  }
}
