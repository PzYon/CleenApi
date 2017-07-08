using CleenApi.Entities;
using CleenApi.Queries.LinqUtilities;

namespace CleenApi.Queries.QueryBuilders
{
  public class DefaultEntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity, DefaultLinqUtility>
    where TEntity : IEntity
  {
  }
}