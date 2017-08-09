using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Library.Web.Hypermedia
{
  public class EntityLinkerCache
  {
    private readonly Lazy<Dictionary<Type, IEntityLinker>> resourceByEntityType
      = new Lazy<Dictionary<Type, IEntityLinker>>(GetLinkerTypesByEntityType);

    public static EntityLinkerCache Instance => _instance ?? (_instance = new EntityLinkerCache());
    private static EntityLinkerCache _instance;

    private EntityLinkerCache()
    {
    }

    public bool AreResourcesEnabled => resourceByEntityType.Value.Any();

    public IEntityLinker GetLinker(Type entityType)
    {
      IEntityLinker entityLinker;
      Type key = resourceByEntityType.Value.Keys.FirstOrDefault(t => t.IsAssignableFrom(entityType));
      return key != null && resourceByEntityType.Value.TryGetValue(key, out entityLinker) ? entityLinker : null;
    }

    private static Dictionary<Type, IEntityLinker> GetLinkerTypesByEntityType()
    {
      var dictionary = new Dictionary<Type, IEntityLinker>();

      foreach (IEntityLinker linkerInstance in AppDomain.CurrentDomain
                                                        .GetAssemblies()
                                                        .SelectMany(a => a.GetTypes())
                                                        .Where(t => typeof(IEntityLinker).IsAssignableFrom(t)
                                                                    && t.IsClass)
                                                        .Select(t => Activator.CreateInstance(t) as IEntityLinker)
                                                        .Where(i => i != null))
      {
        foreach (Type entityType in linkerInstance.GetSupportedEntityTypes())
        {
          dictionary[entityType] = linkerInstance;
        }
      }

      return dictionary;
    }
  }
}