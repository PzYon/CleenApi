using System;
using System.Linq.Expressions;

namespace CleenApi.Queries.ExpressionBuilders
{
  public interface IExpressionBuilder
  {
    Expression<Func<TEntity, bool>> BuildStringCondition<TEntity>(string value,
                                                                  MemberExpression memberExpression,
                                                                  ParameterExpression param);

    Expression<Func<TEntity, bool>> BuildEqualCondition<TEntity>(object value,
                                                                 MemberExpression memberExpression,
                                                                 ParameterExpression param,
                                                                 Type propertyType);
  }
}