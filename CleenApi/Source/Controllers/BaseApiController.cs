using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using CleenApi.Database;
using CleenApi.Entities;

namespace CleenApi.Controllers
{
  public abstract class BaseApiController<TEntity, TEntityChanges> : ApiController
    where TEntity : class, IEntity
    where TEntityChanges : class, IEntityChanges<TEntity>
  {
    protected readonly CleenApiDbContext Db;

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected BaseApiController()
    {
      Db = new CleenApiDbContext();
    }

    public TEntity[] Get()
    {
      return DbSet.ToArray();
    }

    public TEntity Get(int id)
    {
      return DbSet.FirstOrDefault(t => t.Id == id);
    }

    public TEntity Post(TEntityChanges entityChanges)
    {
      var entityToAdd = entityChanges.Id.HasValue
        ? DbSet.FirstOrDefault(e => e.Id == entityChanges.Id.Value)
        : DbSet.Create();

      entityToAdd = entityChanges.ApplyValues(entityToAdd);
      return Db.AddOrUpdate(entityToAdd);
    }

    protected override void Dispose(bool disposing)
    {
      Db.Dispose();
    }
  }
}