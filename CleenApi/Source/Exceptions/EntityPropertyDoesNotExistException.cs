using System;

namespace CleenApi.Exceptions
{
  public class EntityPropertyDoesNotExistException : Exception, IInvalidRequestException
  {
    public EntityPropertyDoesNotExistException(Type entityType, string propertyName)
      : base($"Property '{propertyName}' does not exist on type '{entityType.Name}'.")
    {
    }
  }
}