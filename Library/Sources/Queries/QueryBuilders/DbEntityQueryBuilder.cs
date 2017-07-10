using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.LinqUtilities;

namespace CleenApi.Library.Queries.QueryBuilders
{
  public class DbEntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity, DbLinqUtility>
    where TEntity : IEntity
  {
  }
}