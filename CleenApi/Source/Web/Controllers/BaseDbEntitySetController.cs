using CleenApi.Database;
using CleenApi.Entities;
using CleenApi.Entities.Implementations;

namespace CleenApi.Web.Controllers
{
  public abstract class BaseDbEntitySetController<TEntity, TEntitySet, TEntityChanges, TEntityQuery>
    : BaseEntitySetController<TEntity, TEntitySet, TEntityChanges>
    where TEntity : class, IEntity, new()
    where TEntitySet : BaseDbEntitySet<TEntity, TEntityChanges, TEntityQuery>, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
    where TEntityQuery : class, IEntityQuery<TEntity>, new()
  {
    private readonly CleenApiDbContext db = new CleenApiDbContext();

    protected BaseDbEntitySetController()
    {
      EntitySet.SetDb(db);
    }

    protected override void Dispose(bool disposing)
    {
      EntitySet.Dispose();
      db.Dispose();

      base.Dispose(disposing);
    }
  }
}