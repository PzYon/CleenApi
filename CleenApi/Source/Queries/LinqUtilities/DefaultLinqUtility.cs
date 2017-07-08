using System;
using System.Linq.Expressions;

namespace CleenApi.Queries.LinqUtilities
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

      Expression methodExpression = Expression.Call(memberExpression,
                                                    stringType.GetMethod(methodName,
                                                                         new[] {stringType, stringComparisonType}),
                                                    Expression.Constant(conditionValue, stringType),
                                                    Expression.Constant(StringComparison.OrdinalIgnoreCase));

      if (methodName == nameof(string.IndexOf))
      {
        methodExpression = Expression.GreaterThan(methodExpression, Expression.Constant(0, typeof(int)));
      }

      return Expression.Lambda<Func<TEntity, bool>>(methodExpression, param);
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