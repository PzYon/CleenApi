using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Exceptions;
using CleenApi.Library.Queries;
using CleenApi.Library.Queries.Dynamic;

namespace CleenApi.Library.Web.Controllers
{
  public abstract class BaseEntitySetController<TEntity, TEntitySet, TEntityChanges> : ApiController
    where TEntity : class, IEntity, new()
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
    where TEntityChanges : IEntityChanges<TEntity>
  {
    protected readonly TEntitySet EntitySet = new TEntitySet();

    private readonly Stopwatch watch = Stopwatch.StartNew();

    protected IEntitySetQuery ParseQuery()
    {
      KeyValuePair<string, string>[] pairs = Request.GetQueryNameValuePairs().ToArray();
      return pairs.Any()
               ? new EntitySetQuery(pairs)
               : null;
    }

    public virtual object Get(int id)
    {
      IEntitySetQuery query = ParseQuery();

      IQueryable<object> queryable = EntitySet.Get(query).Where(e => e.Id == id);

      object entity = EnsureSelects(query, queryable).FirstOrDefault();
      if (entity == null)
      {
        throw new EntityNotFoundException<TEntity>(id);
      }

      return entity;
    }

    public virtual object[] Get()
    {
      IEntitySetQuery query = ParseQuery();

      return EnsureSelects(query, EntitySet.Get(query)).ToArray();
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

    private static IQueryable<object> EnsureSelects(IEntitySetQuery query, IQueryable<object> queryable)
    {
      return query != null && query.Selects.Any()
               ? DynamicType.Select<TEntity>(queryable, query.Selects)
               : queryable;
    }
  }
}