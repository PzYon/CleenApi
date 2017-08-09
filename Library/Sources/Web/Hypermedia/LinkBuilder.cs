using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Utilities;

namespace CleenApi.Library.Web.Hypermedia
{
  public class LinkBuilder
  {
    public static dynamic EnsureLinks(object o)
    {
      IDictionary<string, object> expando = new ExpandoObject();

      Type objectType = o.GetType();

      foreach (PropertyInfo pi in objectType.GetProperties())
      {
        object value = ProcessProperty(o, pi);
        expando.Add(pi.Name, value);
      }

      if (typeof(IEntity).IsAssignableFrom(objectType))
      {
        IEntityLinker entityLinker = EntityLinkerCache.Instance.GetLinker(objectType);
        if (entityLinker != null)
        {
          expando["Links"] = entityLinker.GetLinks(o);
        }
      }

      return expando;
    }

    private static object ProcessProperty(object o, PropertyInfo propertyInfo)
    {
      object value = propertyInfo.GetValue(o);
      Type propertyType = propertyInfo.PropertyType;

      if (propertyType.IsByRef)
      {
        return EnsureLinks(value);
      }

      if (propertyType != typeof(string) && ReflectionUtility.IsEnumerable(propertyType))
      {
        var enumerable = value as IEnumerable;
        if (enumerable != null)
        {
          return enumerable.Cast<object>()
                           .Select(EnsureLinks)
                           .ToArray();
        }
      }

      return value;
    }
  }
}
