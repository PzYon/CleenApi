using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CleenApi.Library.Utilities
{
  public static class ExpressionUtility
  {
    public static ParameterExpression CreateParameterExpression(Type type)
    {
      return Expression.Parameter(type, StringUtility.ToCamelCase(type.Name));
    }

    public static MemberAssignment CreateMemberAssignment(FieldInfo p,
                                                          ParameterExpression parameterExpression,
                                                          PropertyInfo sourcePropertyInfo)
    {
      return Expression.Bind(p, Expression.Property(parameterExpression, sourcePropertyInfo));
    }

    public static Expression<Func<TEntity, bool>> CreateOrExpression<TEntity>(Expression<Func<TEntity, bool>> left,
                                                                              Expression<Func<TEntity, bool>> right,
                                                                              ParameterExpression param)
    {
      return Expression.Lambda<Func<TEntity, bool>>(Expression.Or(left.Body, right.Body), param);
    }
  }
}