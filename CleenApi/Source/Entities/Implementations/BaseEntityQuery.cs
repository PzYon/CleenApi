using System.Collections.Generic;
using System.Linq;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations
{
  public abstract class BaseEntityQuery<TEntity> : IEntityQuery<TEntity> where TEntity : class, IEntity
  {
    protected abstract IQueryable<TEntity> HandleIncludes(IQueryable<TEntity> queryable,
                                                          string[] propertiesToInclude);

    protected abstract IQueryable<TEntity> HandleConditions(IQueryable<TEntity> queryable,
                                                            Dictionary<string, string> conditions);

    protected abstract IQueryable<TEntity> HandleOrderBy(IQueryable<TEntity> queryable,
                                                         Dictionary<string, SortDirection> sortFields);

    public IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery query)
    {
      if (query.Skip > 0)
      {
        queryable = queryable.Skip(query.Skip);
      }

      if (query.Take > 0)
      {
        queryable = queryable.Take(query.Take);
      }

      queryable = HandleIncludes(queryable, query.Includes);
      queryable = HandleConditions(queryable, query.Conditions);
      queryable = HandleOrderBy(queryable, query.SortFields);

      return queryable;
    }
  }
}