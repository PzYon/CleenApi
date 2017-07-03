using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using CleenApi.Entities;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Database
{
  public class CleenApiDbContext : DbContext
  {
    public DbSet<Workspace> Workspaces { get; set; }

    public DbSet<User> Users { get; set; }

    public CleenApiDbContext() : base("CleenApi")
    {
      Database.Log = sql => Debug.Write(sql);

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