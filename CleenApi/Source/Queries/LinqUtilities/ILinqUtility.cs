using System.Linq;

namespace CleenApi.Queries.LinqUtilities
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
  }
}