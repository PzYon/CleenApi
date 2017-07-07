using System;

namespace CleenApi.Exceptions
{
  public class EntityPropertyValueTypeNotSupportedException : Exception
  {
    public EntityPropertyValueTypeNotSupportedException(string value, Type targetType)
      : base($"Value '{value}' for type '{targetType.Name}' is not supported.")
    {
    }
  }
}