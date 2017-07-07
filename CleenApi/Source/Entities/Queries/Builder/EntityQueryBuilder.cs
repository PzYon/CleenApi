using CleenApi.Entities.Queries.Builder.Expressions;

namespace CleenApi.Entities.Queries.Builder
{
  public class EntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity>
    where TEntity : IEntity
  {
    protected override IQueryExpressionBuilder ExpressionBuilder => new QueryExpressionBuilder();
  }
}