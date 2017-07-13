using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries;

namespace CleenApi.Library.Web.Controllers
{
  public abstract class BaseEntitySetController<TEntity, TEntitySet, TEntityChanges> : ApiController
    where TEntity : IEntity
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
    where TEntityChanges : IEntityChanges<TEntity>
  {
    protected readonly TEntitySet EntitySet = new TEntitySet();

    private readonly Stopwatch watch = Stopwatch.StartNew();

    protected EntitySetQuery Query
    {
      get
      {
        KeyValuePair<string, string>[] pairs = Request.GetQueryNameValuePairs().ToArray();
        return pairs.Any()
                 ? new EntitySetQuery(pairs)
                 : null;
      }
    }

    public virtual TEntity Get(int id)
    {
      return EntitySet.Get(id, Query?.Includes);
    }

    public virtual TEntity[] Get()
    {
      return EntitySet.Get(Query).ToArray();
    }

    public virtual TEntity Put([FromBody] TEntityChanges entityChanges, int id = 0)
    {
      return EntitySet.Update(entityChanges, id);
    }

    public virtual TEntity Post([FromBody] TEntityChanges entityChanges)
    {
      return EntitySet.Create(entityChanges);
    }

    public virtual void Delete(int id)
    {
      EntitySet.Delete(id);
    }

    protected override void Dispose(bool disposing)
    {
      Debug.WriteLine("Controller lifetime: " + watch.ElapsedMilliseconds + "ms");

      base.Dispose(disposing);
    }
  }
}