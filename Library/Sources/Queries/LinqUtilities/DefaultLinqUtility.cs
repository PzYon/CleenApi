using System;
using System.Linq.Expressions;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public class DefaultLinqUtility : BaseLinqUtility
  {
    protected override Expression<Func<TEntity, bool>> BuildStringCondition<TEntity>(EntityCondition condition,
                                                                                     MemberExpression memberExpression,
                                                                                     ParameterExpression param)
    {
      Type stringType = typeof(string);
      Type stringComparisonType = typeof(StringComparison);

      string methodName = GetStringMethodName(condition.Operator);

      Expression stringComparisonExpression = Expression.Call(memberExpression,
                                                              stringType.GetMethod(methodName,
                                                                                   new[]
                                                                                     {
                                                                                       stringType,
                                                                                       stringComparisonType
                                                                                     }),
                                                              Expression.Constant(condition.Value, stringType),
                                                              Expression.Constant(StringComparison.OrdinalIgnoreCase));

      if (condition.Operator == ConditionOperator.Contains)
      {
        stringComparisonExpression = Expression.GreaterThan(stringComparisonExpression,
                                                            Expression.Constant(-1, typeof(int)));
      }

      BinaryExpression notNullExp = Expression.NotEqual(memberExpression, Expression.Constant(null));

      return Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(notNullExp, stringComparisonExpression), param);
    }

    private static string GetStringMethodName(ConditionOperator op)
    {
      switch (op)
      {
        case ConditionOperator.Contains:
          return nameof(string.IndexOf);
        case ConditionOperator.BeginsWith:
          return nameof(string.StartsWith);
        case ConditionOperator.EndsWith:
          return nameof(string.EndsWith);
        case ConditionOperator.Equals:
          return nameof(string.Equals);
        case ConditionOperator.NotEquals:
        default:
          throw new ArgumentOutOfRangeException(nameof(op), op, null);
      }
    }
  }
}