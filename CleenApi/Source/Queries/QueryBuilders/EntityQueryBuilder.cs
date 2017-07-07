using CleenApi.Entities;
using CleenApi.Queries.ExpressionBuilders;

namespace CleenApi.Queries.QueryBuilders
{
  public class EntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity, ExpressionBuilder>
    where TEntity : IEntity
  {
  }
}