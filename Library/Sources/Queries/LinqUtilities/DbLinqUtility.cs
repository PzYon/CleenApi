using System;
using System.Linq.Expressions;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public class DbLinqUtility : BaseLinqUtility
  {
    protected override Expression<Func<TEntity, bool>> StringCondition<TEntity>(EntityCondition condition,
                                                                                MemberExpression memberExpression,
                                                                                ParameterExpression param)
    {
      Type stringType = typeof(string);

      ConstantExpression valueExpression = Expression.Constant(condition.Value, stringType);
      Expression expression;

      switch (condition.Operator)
      {
        case ConditionOperator.NotEqual:
          expression = Expression.NotEqual(memberExpression, valueExpression);
          break;

        case ConditionOperator.Equal:
          expression = Expression.Equal(memberExpression, valueExpression);
          break;

        default:
          expression = Expression.Call(memberExpression,
                                       stringType.GetMethod(GetStringMethodName(condition.Operator),
                                                            new[] {stringType}),
                                       valueExpression);
          break;
      }

      return Expression.Lambda<Func<TEntity, bool>>(expression, param);
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
        default:
          throw new ArgumentOutOfRangeException(nameof(op), op, null);
      }
    }
  }
}