using System.Data.Entity;
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

    public T AddOrUpdate<T>(T dbo) where T : class, IEntity
    {
      T existingDbo = dbo.Id > 0
                        ? GetById<T>(dbo.Id)
                        : null;

      if (existingDbo == null)
      {
        dbo = Set<T>().Add(dbo);
      }
      else
      {
        // existing item
        Entry(existingDbo).CurrentValues.SetValues(dbo);
        dbo = existingDbo;
      }

      SaveChanges();

      return dbo;
    }

    public virtual T GetById<T>(int id) where T : class, IEntity
    {
      IQueryable<T> set = Set<T>().AsQueryable();

      return set.Single(o => o.Id == id);
    }
  }
}