using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CleenApi.Library.Exceptions;

namespace CleenApi.Library.Queries.LinqUtilities
{
  public abstract class BaseLinqUtility : ILinqUtility
  {
    protected abstract Expression<Func<TEntity, bool>> BuildStringCondition<TEntity>(string value,
                                                                                     MemberExpression memberExpression,
                                                                                     ParameterExpression param);

    public IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> queryable, string propertyName, string value)
    {
      PropertyInfo pi = GetProperty<TEntity>(propertyName);
      Type propertyType = pi.PropertyType;

      object convertedValue = ValueConverter.Convert(value, propertyType);

      ParameterExpression param = Expression.Parameter(typeof(TEntity));
      MemberExpression memberExpression = Expression.Property(param, pi);

      Expression<Func<TEntity, bool>> expression = propertyType == typeof(string)
                                                     ? BuildStringCondition<TEntity>(value,
                                                                                     memberExpression,
                                                                                     param)
                                                     : BuildEqualCondition<TEntity>(convertedValue,
                                                                                    memberExpression,
                                                                                    param,
                                                                                    propertyType);

      return queryable.Where(expression).AsQueryable();
    }

    public IQueryable<TEntity> OrderBy<TEntity>(IQueryable<TEntity> queryable,
                                                string propertyName,
                                                SortDirection sortDirection,
                                                bool isAlreadySorted)
    {
      Type type = typeof(TEntity);
      PropertyInfo pi = GetProperty<TEntity>(propertyName);

      ParameterExpression parameter = Expression.Parameter(type, "p");
      LambdaExpression orderByExp = Expression.Lambda(Expression.MakeMemberAccess(parameter, pi), parameter);

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

    public Expression<Func<TEntity, bool>> BuildEqualCondition<TEntity>(object value,
                                                                        MemberExpression memberExpression,
                                                                        ParameterExpression param,
                                                                        Type propertyType)
    {
      return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(memberExpression,
                                                                     Expression.Constant(value, propertyType)),
                                                    param);
    }

    protected static PropertyInfo GetProperty<TEntity>(string name)
    {
      PropertyInfo pi = typeof(TEntity).GetTypeInfo().GetDeclaredProperty(name);
      if (pi == null)
      {
        throw new EntityPropertyDoesNotExistException(typeof(TEntity), name);
      }

      return pi;
    }
  }
}