using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public interface ILinqUtility
  {
    IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> queryable,
                                       string propertyName,
                                       string value);

    IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> queryable,
                                         string propertyName,
                                         SortDirection sortDirection,
                                         bool isAlreadySorted);

    IQueryable<TEntity> FullText<TEntity>(IQueryable<TEntity> queryable,
                                          string fullText,
                                          IEnumerable<string> propertiesToExclude);
  }
}