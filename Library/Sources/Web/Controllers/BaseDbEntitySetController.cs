using CleenApi.Library.Database;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.Library.Web.Controllers
{
  public abstract class BaseDbEntitySetController<TDbContext, TEntity, TEntitySet, TEntityChanges, TEntityQueryBuilder>
    : BaseEntitySetController<TEntity, TEntitySet, TEntityChanges>
    where TDbContext : BaseDbContext, new()
    where TEntity : class, IEntity
    where TEntitySet : DbEntitySet<TDbContext, TEntity, TEntityChanges, TEntityQueryBuilder>, new()
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : DbEntityQueryBuilder<TEntity>, new()
  {
    private readonly TDbContext db = new TDbContext();

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