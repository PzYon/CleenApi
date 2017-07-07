using CleenApi.Entities.Queries.Builder.Expressions;

namespace CleenApi.Entities.Queries.Builder
{
  public class DbEntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity>
    where TEntity : IEntity
  {
    protected override IQueryExpressionBuilder ExpressionBuilder => new DbQueryExpressionBuilder();
  }
}