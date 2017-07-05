using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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

      object convertedValue = ConvertValue(value, pi.PropertyType);

      ParameterExpression param = Expression.Parameter(typeof(TEntity));

      // todo: adjust binaryExpression based on type
      // (e.g. use Contains for string for example, maybe even add * wilcard)
      BinaryExpression binaryExpression = Expression.Equal(Expression.Property(param, pi),
                                                           Expression.Constant(convertedValue, pi.PropertyType));
      Func<TEntity, bool> conditionExpression = Expression.Lambda<Func<TEntity, bool>>(binaryExpression, param)
                                                          .Compile();

      return queryable.Where(conditionExpression).AsQueryable();
    }

    private static PropertyInfo GetProperty<TEntity>(string name)
    {
      PropertyInfo pi = typeof(TEntity).GetTypeInfo().GetDeclaredProperty(name);
      if (pi == null)
      {
        throw new Exception($"Property '{name}' doesn't exist.");
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

      throw new Exception($"Value '{value}' is not supported.");
    }
  }
}