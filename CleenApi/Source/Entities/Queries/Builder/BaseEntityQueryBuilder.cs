using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities.Queries.Builder
{
  public abstract class BaseEntityQueryBuilder<TEntity> : IEntityQueryBuilder<TEntity> where TEntity : IEntity
  {
    public abstract IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable,
                                                      string[] propertiesToInclude);

    protected abstract IQueryable<TEntity> ApplyConditions(IQueryable<TEntity> queryable,
                                                           Dictionary<string, string> conditions);

    protected abstract IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> queryable,
                                                        Dictionary<string, SortDirection> sortFields);

    public virtual IQueryable<TEntity> ApplyDefaults(IQueryable<TEntity> queryable)
    {
      // do nothing by default. this could be used for conditions which are use for each
      // data access, e.g. permissions check
      return queryable;
    }

    public IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery query)
    {
      queryable = ApplyDefaults(queryable);

      queryable = ApplyIncludes(queryable, query.Includes);
      queryable = ApplyConditions(queryable, query.Conditions);
      queryable = ApplyOrderBy(queryable, query.SortFields);

      if (query.Skip > 0)
      {
        queryable = queryable.Skip(query.Skip);
      }

      if (query.Take > 0)
      {
        queryable = queryable.Take(query.Take);
      }

      return queryable;
    }
  }
}