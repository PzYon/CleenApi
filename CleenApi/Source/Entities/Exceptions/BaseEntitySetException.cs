﻿using System;

namespace CleenApi.Entities.Exceptions
{
  public class BaseEntitySetException<TEntity> : Exception
    where TEntity : IEntity
  {
    public BaseEntitySetException(string message) : base(typeof(TEntity).Name + ": " + message)
    {
    }
  }
}