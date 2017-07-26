using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CleenApi.Library.Utilities;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public abstract class BaseLinqUtility : ILinqUtility
  {
    protected abstract Expression<Func<TEntity, bool>> StringCondition<TEntity>(EntityCondition condition,
                                                                                MemberExpression memberExpression,
                                                                                ParameterExpression param);

    public IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> queryable,
                                              string propertyName,
                                              EntityCondition condition)
    {
      PropertyInfo pi = ReflectionUtility.GetProperty<TEntity>(propertyName);
      Type propertyType = pi.PropertyType;

      ParameterExpression parameterExpression = ExpressionUtility.CreateParameterExpression(typeof(TEntity));
      MemberExpression memberExpression = Expression.Property(parameterExpression, pi);

      Expression<Func<TEntity, bool>> e = propertyType == typeof(string)
                                            ? StringCondition<TEntity>(condition,
                                                                       memberExpression,
                                                                       parameterExpression)
                                            : Condition<TEntity>(condition,
                                                                 memberExpression,
                                                                 parameterExpression,
                                                                 propertyType);

      return queryable.Where(e).AsQueryable();
    }

    public IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> queryable,
                                                string propertyName,
                                                SortDirection sortDirection,
                                                bool isAlreadySorted)
    {
      Type type = typeof(TEntity);
      PropertyInfo pi = ReflectionUtility.GetProperty<TEntity>(propertyName);

      ParameterExpression parameterExpression = ExpressionUtility.CreateParameterExpression(type);
      LambdaExpression orderByExp = Expression.Lambda(Expression.MakeMemberAccess(parameterExpression, pi),
                                                      parameterExpression);

      string methodName = sortDirection == SortDirection.Ascending
                            ? (isAlreadySorted
                                 ? nameof(Queryable.ThenBy)
                                 : nameof(Queryable.OrderBy))
                            : (isAlreadySorted
                                 ? nameof(Queryable.ThenByDescending)
                                 : nameof(Queryable.OrderByDescending));

      MethodCallExpression orderByExpression = Expression.Call(typeof(Queryable),
                                                               methodName,
                                                               new[] {type, pi.PropertyType},
                                                               queryable.Expression,
                                                               Expression.Quote(orderByExp));

      return queryable.Provider.CreateQuery<TEntity>(orderByExpression);
    }

    public IQueryable<TEntity> FullText<TEntity>(IQueryable<TEntity> queryable,
                                                 string fullText,
                                                 IEnumerable<string> propertiesToExclude)
    {
      if (string.IsNullOrEmpty(fullText))
      {
        return queryable;
      }

      Type type = typeof(TEntity);
      ParameterExpression parameterExpression = ExpressionUtility.CreateParameterExpression(type);

      return queryable.Where(type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.PropertyType == typeof(string) && !propertiesToExclude.Contains(p.Name))
                                 .Select(p => StringCondition<TEntity>(new EntityCondition(ConditionOperator.Contains,
                                                                                           fullText),
                                                                       Expression.Property(parameterExpression, p),
                                                                       parameterExpression))
                                 .Aggregate((l, r) => ExpressionUtility.CreateOrExpression(l, r, parameterExpression)))
                      .AsQueryable();
    }

    public Expression<Func<TEntity, bool>> Condition<TEntity>(EntityCondition condition,
                                                              MemberExpression memberExpression,
                                                              ParameterExpression parameterExpression,
                                                              Type propertyType)
    {
      object value = ValueConverter.Convert(condition.Value, propertyType);
      ConstantExpression constantExpression = Expression.Constant(value, propertyType);

      BinaryExpression binaryExpression;

      switch (condition.Operator)
      {
        case ConditionOperator.Equal:
          binaryExpression = Expression.Equal(memberExpression, constantExpression);
          break;
        case ConditionOperator.NotEqual:
          binaryExpression = Expression.NotEqual(memberExpression, constantExpression);
          break;
        default:
          throw new ArgumentException($"{condition.Operator} is not supported.");
      }

      return Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameterExpression);
    }
  }
}