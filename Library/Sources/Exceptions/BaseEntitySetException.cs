using System;
using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Exceptions
{
  public class BaseEntitySetException<TEntity> : Exception
    where TEntity : IEntity
  {
    public BaseEntitySetException(string message) : base(typeof(TEntity).Name + ": " + message)
    {
    }
  }
}