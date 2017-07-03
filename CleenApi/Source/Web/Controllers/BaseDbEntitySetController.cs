using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Database;
using CleenApi.Entities;
using CleenApi.Entities.Queries;

namespace CleenApi.Web.Controllers
{
  public abstract class BaseDbEntitySetController<TEntity, TEntitySet, TEntityChanges> : ApiController
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
  {
    protected readonly TEntitySet EntitySet = new TEntitySet();

    private readonly CleenApiDbContext db = new CleenApiDbContext();

    private readonly Stopwatch watch = Stopwatch.StartNew();

    protected EntitySetQuery EntitySetQuery
    {
      get
      {
        KeyValuePair<string, string>[] pairs = Request.GetQueryNameValuePairs().ToArray();
        return pairs.Any()
                 ? new EntitySetQuery(pairs)
                 : null;
      }
    }

    protected BaseDbEntitySetController()
    {
      EntitySet.SetDb(db);
    }

    public TEntity Get(int id)
    {
      return EntitySet.Get(id, EntitySetQuery?.Includes);
    }

    public TEntity[] Get()
    {
      return EntitySet.Get(EntitySetQuery).ToArray();
    }

    public TEntity Post(TEntityChanges entityChanges)
    {
      return EntitySet.Update(entityChanges);
    }

    public void Delete(int id)
    {
      EntitySet.Delete(id);
    }

    protected override void Dispose(bool disposing)
    {
      EntitySet.Dispose();
      db.Dispose();

      Debug.Write("Controller lifetime: " + watch.ElapsedMilliseconds + "ms");
    }
  }
}