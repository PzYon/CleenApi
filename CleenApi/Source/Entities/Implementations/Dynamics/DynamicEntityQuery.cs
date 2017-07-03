using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations.Dynamics
{
  public class DynamicEntityQuery<TEntity> : BaseEntityQuery<TEntity>
    where TEntity : class, IEntity
  {
    public override IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable,
                                                      string[] includes)
    {
      foreach (string include in includes)
      {
        queryable = queryable.Include(include);
      }

      return queryable;
    }

    protected override IQueryable<TEntity> ApplyConditions(IQueryable<TEntity> queryable,
                                                           Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string propertyName = condition.Key;
        string value = condition.Value;

        queryable = queryable.Where(propertyName, value);
      }

      return queryable;
    }

    protected override IQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> queryable,
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