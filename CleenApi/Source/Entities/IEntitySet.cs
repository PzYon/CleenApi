using System;
using System.Collections.Generic;

namespace CleenApi.Entities
{
  public interface IEntitySet<TEntity, TEntityChanges> : IDisposable
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
  {
    TEntity Get(int id);

    TEntity[] Get(KeyValuePair<string, string>[] conditions);

    TEntity Update(TEntityChanges entityChanges);

    void Delete(int id);
  }
}