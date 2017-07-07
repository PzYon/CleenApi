using CleenApi.Entities;
using CleenApi.Queries.ExpressionBuilders;

namespace CleenApi.Queries.QueryBuilders
{
  public class DbEntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity, DbExpressionBuilder>
    where TEntity : IEntity
  {
  }
}