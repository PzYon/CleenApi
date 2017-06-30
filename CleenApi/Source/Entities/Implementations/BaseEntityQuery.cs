using System.Collections.Generic;
using System.Linq;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations
{
  public abstract class BaseEntityQuery<TEntity> : IEntityQuery<TEntity> where TEntity : class, IEntity
  {
    protected abstract IQueryable<TEntity> HandleConditions(IQueryable<TEntity> set,
                                                            Dictionary<string, string> conditions);

    protected abstract IQueryable<TEntity> HandleOrderBy(IQueryable<TEntity> set,
                                                         Dictionary<string, SortDirection> sortFields);

    protected abstract IQueryable<TEntity> HandleIncludes(IQueryable<TEntity> set,
                                                          string[] propertiesToInclude);

    public IQueryable<TEntity> Build(IQueryable<TEntity> set, EntitySetQuery query)
    {
      if (query.Skip > 0)
      {
        set = set.Skip(query.Skip);
      }

      if (query.Take > 0)
      {
        set = set.Take(query.Take);
      }

      set = HandleConditions(set, query.Conditions);
      set = HandleOrderBy(set, query.SortFields);
      set = HandleIncludes(set, query.Includes);

      return set;
    }
  }
}