using System.Reflection;
using CleenApi.Library.Exceptions;

namespace CleenApi.Library.Utilities
{
  public static class ReflectionUtility
  {
    public static PropertyInfo GetProperty<TEntity>(string name)
    {
      PropertyInfo pi = typeof(TEntity).GetTypeInfo().GetDeclaredProperty(name);
      if (pi == null)
      {
        throw new EntityPropertyDoesNotExistException(typeof(TEntity), name);
      }

      return pi;
    }
  }
}