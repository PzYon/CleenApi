using CleenApi.Database;
using CleenApi.Entities;
using CleenApi.Entities.Implementations;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Web.Controllers
{
  public abstract class BaseDbEntitySetController<TEntity, TEntitySet, TEntityChanges, TEntityQueryBuilder>
    : BaseEntitySetController<TEntity, TEntitySet, TEntityChanges>
    where TEntity : class, IEntity
    where TEntitySet : DbEntitySet<TEntity, TEntityChanges, TEntityQueryBuilder>, new()
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : DbEntityQueryBuilder<TEntity>, new()
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