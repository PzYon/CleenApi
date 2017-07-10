using System.Data.Entity;
using System.Linq;
using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Database
{
  public class BaseDbContext : DbContext
  {
    public BaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
    {
      Configuration.LazyLoadingEnabled = false;
    }

    public T AddOrUpdate<T>(T entity) where T : class, IEntity
    {
      T existingEntity = entity.Id > 0
                           ? GetById<T>(entity.Id)
                           : null;

      if (existingEntity == null)
      {
        entity = Set<T>().Add(entity);
      }
      else
      {
        Entry(existingEntity).CurrentValues.SetValues(entity);
        entity = existingEntity;
      }

      SaveChanges();

      return entity;
    }

    public T GetById<T>(int id) where T : class, IEntity
    {
      return Set<T>().Single(o => o.Id == id);
    }
  }
}