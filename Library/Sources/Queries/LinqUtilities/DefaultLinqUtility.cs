using System;
using System.Linq.Expressions;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public class DefaultLinqUtility : BaseLinqUtility
  {
    protected override Expression<Func<TEntity, bool>> StringCondition<TEntity>(EntityCondition condition,
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

      switch (condition.Operator)
      {
        case ConditionOperator.Contains:
          stringComparisonExpression = Expression.GreaterThan(stringComparisonExpression,
                                                              Expression.Constant(-1, typeof(int)));
          break;
        case ConditionOperator.NotEqual:
          stringComparisonExpression = Expression.Not(stringComparisonExpression);
          break;
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
        case ConditionOperator.Equal:
        case ConditionOperator.NotEqual:
          return nameof(string.Equals);
        default:
          throw new ArgumentOutOfRangeException(nameof(op), op, null);
      }
    }
  }
}