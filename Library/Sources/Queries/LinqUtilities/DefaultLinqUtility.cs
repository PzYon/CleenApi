using System;
using System.Linq.Expressions;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public class DefaultLinqUtility : BaseLinqUtility
  {
    protected override Expression<Func<TEntity, bool>> BuildStringCondition<TEntity>(string value,
                                                                                     MemberExpression memberExpression,
                                                                                     ParameterExpression param)
    {
      Type stringType = typeof(string);
      Type stringComparisonType = typeof(StringComparison);

      string conditionValue = value.TrimEnd('*').TrimStart('*');

      string methodName = GetStringMethodName(value);

      Expression stringComparisonExpression = Expression.Call(memberExpression,
                                                              stringType.GetMethod(methodName,
                                                                                   new[]
                                                                                     {
                                                                                       stringType,
                                                                                       stringComparisonType
                                                                                     }),
                                                              Expression.Constant(conditionValue, stringType),
                                                              Expression.Constant(StringComparison.OrdinalIgnoreCase));

      if (methodName == nameof(string.IndexOf))
      {
        stringComparisonExpression = Expression.GreaterThan(stringComparisonExpression,
                                                            Expression.Constant(-1, typeof(int)));
      }

      BinaryExpression notNullExp = Expression.NotEqual(memberExpression, Expression.Constant(null));

      return Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(notNullExp, stringComparisonExpression), param);
    }

    private static string GetStringMethodName(string value)
    {
      if (value.StartsWith("*") && value.EndsWith("*"))
      {
        return nameof(string.IndexOf);
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