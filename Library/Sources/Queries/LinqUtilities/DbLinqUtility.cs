using System;
using System.Linq.Expressions;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public class DbLinqUtility : BaseLinqUtility
  {
    protected override Expression<Func<TEntity, bool>> BuildStringCondition<TEntity>(EntityCondition condition,
                                                                                     MemberExpression memberExpression,
                                                                                     ParameterExpression param)
    {
      Type stringType = typeof(string);

      string methodName = GetStringMethodName(condition.Operator);

      MethodCallExpression methodExpression = Expression.Call(memberExpression,
                                                              stringType.GetMethod(methodName, new[] {stringType}),
                                                              Expression.Constant(condition.Value, stringType));

      return Expression.Lambda<Func<TEntity, bool>>(methodExpression, param);
    }

    private static string GetStringMethodName(ConditionOperator op)
    {
      switch (op)
      {
        case ConditionOperator.Contains:
          return nameof(string.Contains);
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