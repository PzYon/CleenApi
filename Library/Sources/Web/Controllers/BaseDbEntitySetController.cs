using CleenApi.Library.Database;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.Library.Web.Controllers
{
  public abstract class BaseDbEntitySetController<TDbContext, TEntity, TEntitySet, TEntityChanges, TEntityQueryBuilder>
    : BaseEntitySetController<TEntity, TEntitySet, TEntityChanges>
    where TDbContext : BaseDbContext, new()
    where TEntity : class, IEntity, new()
    where TEntitySet : DbEntitySet<TDbContext, TEntity, TEntityChanges, TEntityQueryBuilder>, new()
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : DbEntityQueryBuilder<TEntity>, new()
  {
    protected readonly TDbContext Db = new TDbContext();

    protected BaseDbEntitySetController()
    {
      EntitySet.SetDb(Db);
    }

    protected override void Dispose(bool disposing)
    {
      EntitySet.Dispose();
      Db.Dispose();

      base.Dispose(disposing);
    }
  }
}