using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Exceptions;
using CleenApi.Library.Queries.LinqUtilities;

namespace CleenApi.Library.Queries.QueryBuilders
{
  public abstract class BaseEntityQueryBuilder<TEntity, TLinqUtility> : IEntityQueryBuilder<TEntity>
    where TEntity : IEntity
    where TLinqUtility : class, ILinqUtility, new()
  {
    protected readonly ILinqUtility LinqUtility = new TLinqUtility();

    public virtual IQueryable<TEntity> Build(IQueryable<TEntity> queryable,
                                             IEntitySetQuery query = null)
    {
      queryable = ApplyDefaults(queryable);

      if (query == null)
      {
        return queryable;
      }

      queryable = ApplyIncludes(queryable, query.Includes);
      queryable = ApplyConditions(queryable, query.Conditions);
      queryable = ApplyFullText(queryable, query.FullText);
      queryable = ApplyOrderBy(queryable, query.SortFields);

      if (query.Skip > 0)
      {
        if (!query.SortFields.Any())
        {
          throw new InvalidRequestException("$skip can only be applied in combination with $orderBy");
        }

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

    public IQueryable<TEntity> ApplyFullText(IQueryable<TEntity> queryable, string fullText)
    {
      return LinqUtility.FullText(queryable, fullText, GetPropertiesToExcludeFromFullText());
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
                                                          Dictionary<string, EntityCondition> conditions)
    {
      foreach (KeyValuePair<string, EntityCondition> condition in conditions)
      {
        string propertyName = condition.Key;
        EntityCondition entityCondition = condition.Value;

        queryable = LinqUtility.Where(queryable, propertyName, entityCondition);
      }

      return queryable;
    }

    protected virtual IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> queryable,
                                                       Dictionary<string, SortDirection> sortFields)
    {
      var isAlreadySorted = false;

      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string propertyName = sortField.Key;
        SortDirection sortDirection = sortField.Value;

        queryable = LinqUtility.OrderBy(queryable, propertyName, sortDirection, isAlreadySorted);

        isAlreadySorted = true;
      }

      return queryable;
    }

    public virtual IEnumerable<string> GetPropertiesToExcludeFromFullText()
    {
      yield break;
    }
  }
}