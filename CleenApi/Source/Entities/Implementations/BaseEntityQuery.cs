using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities.Implementations
{
  public abstract class BaseEntityQuery<TEntity> : IEntityQuery<TEntity> where TEntity : class, IEntity
  {
    public IQueryable<TEntity> Build(IQueryable<TEntity> set, EntitySetQuery entitySetQuery)
    {
      if (entitySetQuery.Skip > 0)
      {
        set = set.Skip(entitySetQuery.Skip);
      }

      if (entitySetQuery.Take > 0)
      {
        set = set.Take(entitySetQuery.Take);
      }

      set = HandleConditions(set, entitySetQuery.Conditions);

      return set;
    }

    protected abstract IQueryable<TEntity> HandleConditions(IQueryable<TEntity> set,
                                                            Dictionary<string, string> conditions);
  }
}