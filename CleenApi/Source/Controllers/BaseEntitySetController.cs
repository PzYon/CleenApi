using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CleenApi.Entities;

namespace CleenApi.Controllers
{
  public abstract class BaseEntitySetController<TEntitySet, TEntity, TEntityChanges> : ApiController
    where TEntitySet : class, IEntitySet<TEntity, TEntityChanges>, new()
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
  {
    protected TEntitySet Repo { get; }

    protected BaseEntitySetController()
    {
      Repo = new TEntitySet();
    }

    public TEntity Get(int id)
    {
      return Repo.Get(id);
    }

    public TEntity[] Get()
    {
      return Repo.Get(Request.GetQueryNameValuePairs().ToArray());
    }

    public TEntity Post(TEntityChanges entityChanges)
    {
      return Repo.Update(entityChanges);
    }

    public void Delete(int id)
    {
      Repo.Delete(id);
    }

    protected override void Dispose(bool disposing)
    {
      Repo.Dispose();
    }
  }
}