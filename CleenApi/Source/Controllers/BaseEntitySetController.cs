using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Database;
using CleenApi.Entities;

namespace CleenApi.Controllers
{
  public abstract class BaseEntitySetController<TEntitySet, TEntity, TEntityChanges> : ApiController
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
  {
    protected TEntitySet EntitySet { get; }

    private readonly CleenApiDbContext db;

    protected BaseEntitySetController()
    {
      db = new CleenApiDbContext();
      EntitySet = new TEntitySet();
      EntitySet.SetDb(db);
    }

    public TEntity Get(int id)
    {
      return EntitySet.Get(id);
    }

    public TEntity[] Get()
    {
      return EntitySet.Get(ParseUrlConditions());
    }

    public TEntity Post(TEntityChanges entityChanges)
    {
      return EntitySet.Update(entityChanges);
    }

    public void Delete(int id)
    {
      EntitySet.Delete(id);
    }

    protected KeyValuePair<string, string>[] ParseUrlConditions()
    {
      return Request.GetQueryNameValuePairs().ToArray();
    }

    protected override void Dispose(bool disposing)
    {
      EntitySet.Dispose();
      db.Dispose();
    }
  }
}