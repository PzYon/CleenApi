using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CleenApi.Library.Web.Hypermedia
{
  public static class LinkUtility
  {
    public static bool AreResourceLinksEnabled => EntityLinkerCache.Instance.AreResourcesEnabled;

    public static Link BuildLink<TController>(int id, string rel, string methodName, Type[] parameterTypes = null)
      where TController : ApiController
    {
      Link link = BuildLink<TController>(rel, methodName, parameterTypes);
      link.Href = link.Href.Replace("{workspaceId}", id.ToString());

      return link;
    }

    public static Link BuildLink<TController>(string rel, string methodName, Type[] parameterTypes = null)
      where TController : ApiController
    {
      Type t = typeof(TController);

      MethodInfo[] matchingMethods = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
      MethodInfo matchingMethod;

      if (parameterTypes != null)
      {
        matchingMethod = matchingMethods.SingleOrDefault(x => x.Name == methodName
                                                              && x.GetParameters()
                                                                  .Select(p => p.ParameterType)
                                                                  .SequenceEqual(parameterTypes));
      }
      else
      {
        matchingMethod = matchingMethods.FirstOrDefault();
      }

      if (matchingMethod == null)
      {
        // todo!
        throw new Exception();
      }

      string method = GetHttpMethod(matchingMethod);
      string href = GetHref(matchingMethod);

      return new Link
        {
          Href = href,
          Method = method,
          Rel = rel
        };
    }

    private static string GetHref(MethodInfo m)
    {
      RouteAttribute routeAttribute = m.GetCustomAttributes()
                                       .OfType<RouteAttribute>()
                                       .FirstOrDefault();

      if (routeAttribute == null)
      {
        return "not defined as default route";
        throw new ArgumentException("this can return null if default route... "
                                    + "handle based on method name (Post, Get, etc)");
      }

      return routeAttribute.Template;
    }

    private static string GetHttpMethod(MethodInfo m)
    {
      IActionHttpMethodProvider methodProvider = m.GetCustomAttributes()
                                                  .OfType<IActionHttpMethodProvider>()
                                                  .FirstOrDefault();

      if (methodProvider != null)
      {
        // todo: what if there are more than one HttpMethod? is this even possible?
        return methodProvider.HttpMethods.First().Method;
      }

      return HttpMethod.Get.Method;
    }
  }
}