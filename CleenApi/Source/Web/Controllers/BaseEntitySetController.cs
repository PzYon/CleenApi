using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Entities;
using CleenApi.Entities.Queries;

namespace CleenApi.Web.Controllers
{
  public abstract class BaseEntitySetController<TEntity, TEntitySet, TEntityChanges> : ApiController
    where TEntity : class, IEntity, new()
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
  {
    protected readonly TEntitySet EntitySet = new TEntitySet();

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
      Debug.Write("Controller lifetime: " + watch.ElapsedMilliseconds + "ms");

      base.Dispose(disposing);
    }
  }
}