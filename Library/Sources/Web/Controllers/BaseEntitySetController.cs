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

    public TEntity Get(int id)
    {
      return EntitySet.Get(id, Query?.Includes);
    }

    public TEntity[] Get()
    {
      return EntitySet.Get(Query).ToArray();
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
      Debug.WriteLine("Controller lifetime: " + watch.ElapsedMilliseconds + "ms");

      base.Dispose(disposing);
    }
  }
}