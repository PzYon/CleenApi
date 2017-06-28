using System.Web.Http;
using CleenApi.Database;

namespace CleenApi.Startup
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});

      config.Formatters.Remove(config.Formatters.XmlFormatter);

      System.Data.Entity.Database.SetInitializer(new CleenApiDbInitializer());
    }
  }
}
