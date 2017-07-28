using System;
using System.Linq;
using CleenApi.Library.Queries;

namespace CleenApi.Library.EntitySets
{
  public interface IEntitySet<TEntity, TEntityChanges> : IDisposable
    where TEntity : IEntity
    where TEntityChanges : IEntityChanges<TEntity>
  {
    IQueryable<TEntity> Get(IEntitySetQuery query);

    TEntity Get(int id);

    TEntity Update(TEntityChanges entityChanges, int id = 0);

    TEntity Create(TEntityChanges entityChanges);

    void Delete(int id);
  }
}