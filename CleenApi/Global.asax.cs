﻿using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using CleenApi.Database;
using CleenApi.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CleenApi
{
  public class WebApiApplication : HttpApplication
  {
    protected void Application_Start()
    {
      System.Data.Entity.Database.SetInitializer(new CleenApiDbInitializer());

      GlobalConfiguration.Configure(SetupWebApi);
    }

    private static void SetupWebApi(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();
      config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new {id = RouteParameter.Optional});

      config.Services.Replace(typeof(IExceptionHandler), new CleenApiExceptionHandler());

      config.Formatters.Remove(config.Formatters.XmlFormatter);

      config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
        {
          ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }
  }
}