using System;
using System.Linq.Expressions;

namespace CleenApi.Queries.ExpressionBuilders
{
  public abstract class BaseExpressionBuilder : IExpressionBuilder
  {
    public abstract Expression<Func<TEntity, bool>> BuildStringCondition<TEntity>(string value,
                                                                                  MemberExpression memberExpression,
                                                                                  ParameterExpression param);

    public Expression<Func<TEntity, bool>> BuildEqualCondition<TEntity>(object value,
                                                                        MemberExpression memberExpression,
                                                                        ParameterExpression param,
                                                                        Type propertyType)
    {
      return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(memberExpression,
                                                                     Expression.Constant(value, propertyType)),
                                                    param);
    }
  }
}