using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleenApi.Entities;
using CleenApi.Queries.ExpressionBuilders;

namespace CleenApi.Queries.QueryBuilders
{
  public abstract class BaseEntityQueryBuilder<TEntity, TExpressionBuilder> : IEntityQueryBuilder<TEntity>
    where TEntity : IEntity
    where TExpressionBuilder : class, IExpressionBuilder, new()
  {
    protected readonly IExpressionBuilder ExpressionBuilder = new TExpressionBuilder();

    public virtual IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery query = null)
    {
      queryable = ApplyDefaults(queryable);

      if (query == null)
      {
        return queryable;
      }

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

    public virtual IQueryable<TEntity> ApplyDefaults(IQueryable<TEntity> queryable)
    {
      // do nothing by default. this could be used for conditions which are use for each
      // data access, e.g. permissions check
      return queryable;
    }

    public virtual IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable,
                                                     string[] includes)
    {
      foreach (string include in includes)
      {
        queryable = queryable.Include(include);
      }

      return queryable;
    }

    protected virtual IQueryable<TEntity> ApplyConditions(IQueryable<TEntity> queryable,
                                                          Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string propertyName = condition.Key;
        string value = condition.Value;

        queryable = queryable.Where(propertyName, value, ExpressionBuilder);
      }

      return queryable;
    }

    protected virtual IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> queryable,
                                                       Dictionary<string, SortDirection> sortFields)
    {
      var isAlreadyOrdered = false;

      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string propertyName = sortField.Key;
        SortDirection sortDirection = sortField.Value;

        queryable = queryable.OrderBy(propertyName, sortDirection, isAlreadyOrdered);

        isAlreadyOrdered = true;
      }

      return queryable;
    }
  }
}