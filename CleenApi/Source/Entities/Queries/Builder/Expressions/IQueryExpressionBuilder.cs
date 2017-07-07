using System;
using System.Linq.Expressions;

namespace CleenApi.Entities.Queries.Builder.Expressions
{
  public interface IQueryExpressionBuilder
  {
    Expression<Func<TEntity, bool>> GetStringCondition<TEntity>(string value,
                                                                MemberExpression memberExpression,
                                                                ParameterExpression param);
  }
}