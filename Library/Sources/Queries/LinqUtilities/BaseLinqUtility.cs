using System;
using System.Collections.Generic;
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

      ParameterExpression parameterExpression = CreateParameterExpression(typeof(TEntity));
      MemberExpression memberExpression = Expression.Property(parameterExpression, pi);

      Expression<Func<TEntity, bool>> e = propertyType == typeof(string)
                                            ? BuildStringCondition<TEntity>(value,
                                                                            memberExpression,
                                                                            parameterExpression)
                                            : BuildEqualCondition<TEntity>(ValueConverter.Convert(value, propertyType),
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
      PropertyInfo pi = GetProperty<TEntity>(propertyName);

      ParameterExpression parameterExpression = CreateParameterExpression(type);
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
      Type type = typeof(TEntity);
      ParameterExpression parameterExpression = CreateParameterExpression(type);

      return queryable.Where(type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.PropertyType == typeof(string)
                                             && !propertiesToExclude.Contains(p.Name))
                                 .Select(p => BuildStringCondition<TEntity>("*" + fullText + "*",
                                                                            Expression.Property(parameterExpression,
                                                                                                p),
                                                                            parameterExpression))
                                 .Aggregate((l, r) => CreateOrExpression(l, r, parameterExpression)))
                      .AsQueryable();
    }

    public Expression<Func<TEntity, bool>> BuildEqualCondition<TEntity>(object value,
                                                                        MemberExpression memberExpression,
                                                                        ParameterExpression parameterExpression,
                                                                        Type propertyType)
    {
      return Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(memberExpression,
                                                                     Expression.Constant(value, propertyType)),
                                                    parameterExpression);
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

    protected static ParameterExpression CreateParameterExpression(Type type)
    {
      return Expression.Parameter(type, type.Name.ToLower());
    }

    private static Expression<Func<TEntity, bool>> CreateOrExpression<TEntity>(Expression<Func<TEntity, bool>> left,
                                                                               Expression<Func<TEntity, bool>> right,
                                                                               ParameterExpression param)
    {
      return Expression.Lambda<Func<TEntity, bool>>(Expression.Or(left.Body, right.Body), param);
    }
  }
}