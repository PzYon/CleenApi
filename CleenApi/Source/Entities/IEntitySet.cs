using System;
using System.Collections.Generic;
using CleenApi.Database;

namespace CleenApi.Entities
{
  public interface IEntitySet<TEntity, TEntityChanges> : IDisposable
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
  {
    void SetDb(CleenApiDbContext db);

    TEntity Get(int id);

    TEntity[] Get(KeyValuePair<string, string>[] conditions = null);

    TEntity Update(TEntityChanges entityChanges);

    void Delete(int id);
  }
}