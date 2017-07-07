using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CleenApi.Exceptions;

namespace CleenApi.Entities.Queries.Builder
{
  public static class LinqExtensions
  {
    public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> queryable,
                                                       string propertyName,
                                                       SortDirection sortDirection,
                                                       bool isAlreadyOrdered)
    {
      Type type = typeof(TEntity);

      PropertyInfo pi = GetProperty<TEntity>(propertyName);

      ParameterExpression parameter = Expression.Parameter(type, "p");
      LambdaExpression orderByExp = Expression.Lambda(Expression.MakeMemberAccess(parameter, pi),
                                                      parameter);

      string methodName = sortDirection == SortDirection.Ascending
                            ? (isAlreadyOrdered
                                 ? nameof(Queryable.ThenBy)
                                 : nameof(Queryable.OrderBy))
                            : (isAlreadyOrdered
                                 ? nameof(Queryable.ThenByDescending)
                                 : nameof(Queryable.OrderByDescending));

      MethodCallExpression orderByExpression = Expression.Call(typeof(Queryable),
                                                               methodName,
                                                               new[] {type, pi.PropertyType},
                                                               queryable.Expression,
                                                               Expression.Quote(orderByExp));

      return queryable.Provider.CreateQuery<TEntity>(orderByExpression);
    }

    public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> queryable,
                                                     string propertyName,
                                                     string value)
    {
      PropertyInfo pi = GetProperty<TEntity>(propertyName);
      Type propertyType = pi.PropertyType;

      object convertedValue = ConvertValue(value, propertyType);

      ParameterExpression param = Expression.Parameter(typeof(TEntity));
      MemberExpression memberExpression = Expression.Property(param, pi);

      Expression<Func<TEntity, bool>> expression = propertyType == typeof(string)
                                                     ? GetStringCondition<TEntity>(value,
                                                                                   memberExpression,
                                                                                   param)
                                                     : GetCondition<TEntity>(convertedValue,
                                                                             memberExpression,
                                                                             propertyType,
                                                                             param);

      return queryable.Where(expression).AsQueryable();
    }

    private static Expression<Func<TEntity, bool>> GetCondition<TEntity>(object value,
                                                                         MemberExpression memberExpression,
                                                                         Type propertyType,
                                                                         ParameterExpression param)
    {
      return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(memberExpression,
                                                                     Expression.Constant(value, propertyType)),
                                                    param);
    }

    private static Expression<Func<TEntity, bool>> GetStringCondition<TEntity>(string value,
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

    private static PropertyInfo GetProperty<TEntity>(string name)
    {
      PropertyInfo pi = typeof(TEntity).GetTypeInfo().GetDeclaredProperty(name);
      if (pi == null)
      {
        throw new EntityPropertyDoesNotExistException(typeof(TEntity), name);
      }

      return pi;
    }

    private static object ConvertValue(string value, Type targetType)
    {
      if (targetType == typeof(string))
      {
        return value;
      }

      if (targetType == typeof(int))
      {
        return Convert.ToInt32(value);
      }

      if (targetType == typeof(bool))
      {
        return Convert.ToBoolean(value);
      }

      throw new EntityPropertyValueTypeNotSupportedException(value, targetType);
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