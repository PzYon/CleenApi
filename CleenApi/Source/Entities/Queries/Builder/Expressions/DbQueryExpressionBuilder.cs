using System;
using System.Linq.Expressions;

namespace CleenApi.Entities.Queries.Builder.Expressions
{
  public class DbQueryExpressionBuilder : IQueryExpressionBuilder
  {
    public Expression<Func<TEntity, bool>> GetStringCondition<TEntity>(string value,
                                                                       MemberExpression memberExpression,
                                                                       ParameterExpression param)
    {
      Type stringType = typeof(string);

      string conditionValue = value.TrimEnd('*').TrimStart('*');

      MethodCallExpression methodExpression = Expression.Call(memberExpression,
                                                              stringType.GetMethod(GetStringMethodName(value),
                                                                                   new[] {stringType}),
                                                              Expression.Constant(conditionValue, stringType));

      return Expression.Lambda<Func<TEntity, bool>>(methodExpression, param);
    }

    private static string GetStringMethodName(string value)
    {
      if (value.StartsWith("*") && value.EndsWith("*"))
      {
        return nameof(string.Contains);
      }

      if (value.EndsWith("*"))
      {
        return nameof(string.StartsWith);
      }

      if (value.StartsWith("*"))
      {
        return nameof(string.EndsWith);
      }

      return nameof(string.Equals);
    }
  }
}