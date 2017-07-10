using System;

namespace CleenApi.Library.Exceptions
{
  public class EntityPropertyValueTypeNotSupportedException : Exception, IInvalidRequestException
  {
    public EntityPropertyValueTypeNotSupportedException(string value, Type targetType)
      : base($"Value '{value}' for target type '{targetType.Name}' is not supported.")
    {
    }
  }
}