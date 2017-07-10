using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.LinqUtilities;

namespace CleenApi.Library.Queries.QueryBuilders
{
  public class DefaultEntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity, DefaultLinqUtility>
    where TEntity : IEntity
  {
  }
}