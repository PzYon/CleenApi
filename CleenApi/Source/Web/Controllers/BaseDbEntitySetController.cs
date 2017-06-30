using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Database;
using CleenApi.Entities;
using CleenApi.Entities.Queries;

namespace CleenApi.Web.Controllers
{
  public abstract class BaseDbEntitySetController<TEntitySet, TEntity, TEntityChanges> : ApiController
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
  {
    protected readonly TEntitySet EntitySet = new TEntitySet();

    private readonly CleenApiDbContext db = new CleenApiDbContext();

    protected BaseDbEntitySetController()
    {
      EntitySet.SetDb(db);
    }

    public TEntity Get(int id)
    {
      return EntitySet.Get(id);
    }

    public TEntity[] Get()
    {
      return EntitySet.Get(GetEntitySetQuery()).ToArray();
    }

    public TEntity Post(TEntityChanges entityChanges)
    {
      return EntitySet.Update(entityChanges);
    }

    public void Delete(int id)
    {
      EntitySet.Delete(id);
    }

    protected EntitySetQuery GetEntitySetQuery()
    {
      KeyValuePair<string, string>[] pair = Request.GetQueryNameValuePairs().ToArray();
      return pair.Any()
               ? new EntitySetQuery(pair)
               : null;
    }

    protected override void Dispose(bool disposing)
    {
      EntitySet.Dispose();
      db.Dispose();
    }
  }
}