using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations.Dynamics
{
  public class DynamicEntityQuery<TEntity> : IEntityQuery<TEntity>
    where TEntity : class, IEntity
  {
    public IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery entitySetQuery)
    {
      queryable = HandleIncludes(queryable, entitySetQuery.Includes);
      queryable = HandleConditions(queryable, entitySetQuery.Conditions);
      queryable = HandleOrderBy(queryable, entitySetQuery.SortFields);

      return queryable;
    }

    private static IQueryable<TEntity> HandleIncludes(IQueryable<TEntity> set, string[] includes)
    {
      foreach (string include in includes)
      {
        set = set.Include(include);
      }

      return set;
    }

    private static IQueryable<TEntity> HandleConditions(IQueryable<TEntity> set, Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string propertyName = condition.Key;
        string value = condition.Value;

        set = set.Where(propertyName, value);
      }

      return set;
    }

    private static IQueryable<TEntity> HandleOrderBy(IQueryable<TEntity> set,
                                                     Dictionary<string, SortDirection> sortFields)
    {
      var isAlreadyOrdered = false;

      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string propertyName = sortField.Key;
        SortDirection sortDirection = sortField.Value;

        set = set.OrderBy(propertyName, sortDirection, isAlreadyOrdered);

        isAlreadyOrdered = true;
      }

      return set;
    }
  }
}