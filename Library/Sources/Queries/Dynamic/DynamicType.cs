using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Utilities;

namespace CleenApi.Library.Queries.Dynamic
{
  public static class DynamicType
  {
    private static readonly AssemblyName assemblyName = new AssemblyName {Name = "CleenApi.DynamicTypes"};

    private static readonly ModuleBuilder moduleBuilder = Thread.GetDomain()
                                                                .DefineDynamicAssembly(assemblyName,
                                                                                       AssemblyBuilderAccess.Run)
                                                                .DefineDynamicModule(assemblyName.Name);

    private static readonly Dictionary<string, Type> typeCache = new Dictionary<string, Type>();

    public static IQueryable<object> Select<TEntity>(IQueryable<object> source, IEnumerable<string> propertyNames)
      where TEntity : IEntity
    {
      Type entityType = typeof(TEntity);
      ParameterExpression parameterExpression = ExpressionUtility.CreateParameterExpression(entityType);

      Dictionary<string, PropertyInfo> propertyInfosByName = GetPropertyInfosByName<TEntity>(propertyNames);

      Type dynamicType = Build(propertyInfosByName.Values.ToDictionary(p => p.Name, p => p.PropertyType));

      NewExpression newExpression = Expression.New(dynamicType.GetConstructor(Type.EmptyTypes));
      MemberBinding[] memberBindings = GetMemberBindings(dynamicType, parameterExpression, propertyInfosByName);
      MemberInitExpression memberInitExpression = Expression.MemberInit(newExpression, memberBindings);

      Expression selector = Expression.Lambda(memberInitExpression, parameterExpression);

      return source.Provider
                   .CreateQuery<object>(Expression.Call(typeof(Queryable),
                                                        nameof(Queryable.Select),
                                                        new[] {entityType, dynamicType},
                                                        Expression.Constant(source),
                                                        selector));
    }

    private static Dictionary<string, PropertyInfo> GetPropertyInfosByName<TEntity>(IEnumerable<string> propertyNames) where TEntity : IEntity
    {
      var propertyInfosByName = new Dictionary<string, PropertyInfo>();

      foreach (string propertyName in propertyNames.Distinct())
      {
        propertyInfosByName[propertyName] = ReflectionUtility.GetProperty<TEntity>(propertyName);
      }

      const string idName = nameof(IEntity.Id);
      if (!propertyInfosByName.ContainsKey(idName))
      {
        propertyInfosByName[idName] = ReflectionUtility.GetProperty<TEntity>(idName);
      }

      return propertyInfosByName;
    }

    private static MemberBinding[] GetMemberBindings(Type dynamicType,
                                                     ParameterExpression parameterExpression,
                                                     IReadOnlyDictionary<string, PropertyInfo> propertiesByName)
    {
      return dynamicType.GetFields()
                        .Select(p => ExpressionUtility.CreateMemberAssignment(p,
                                                                              parameterExpression,
                                                                              propertiesByName[p.Name]))
                        .Cast<MemberBinding>()
                        .ToArray();
    }

    private static Type Build(Dictionary<string, Type> fieldTypeByFieldName)
    {
      try
      {
        Monitor.Enter(typeCache);

        string className = fieldTypeByFieldName.OrderBy(f => f.Key)
                                               .Aggregate(string.Empty,
                                                          (current, field) => string.Join("|",
                                                                                          current,
                                                                                          GetFieldLabel(field)));

        if (typeCache.ContainsKey(className))
        {
          return typeCache[className];
        }

        TypeBuilder typeBuilder = moduleBuilder.DefineType(className,
                                                           TypeAttributes.Public
                                                           | TypeAttributes.Class
                                                           | TypeAttributes.Serializable);

        foreach (KeyValuePair<string, Type> field in fieldTypeByFieldName)
        {
          typeBuilder.DefineField(field.Key, field.Value, FieldAttributes.Public);
        }

        typeCache[className] = typeBuilder.CreateType();

        return typeCache[className];
      }
      finally
      {
        Monitor.Exit(typeCache);
      }
    }

    private static string GetFieldLabel(KeyValuePair<string, Type> field)
    {
      return $"{field.Key}:{field.Value.Name}";
    }
  }
}