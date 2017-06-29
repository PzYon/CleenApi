using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CleenApi.Database;
using CleenApi.Entities;

namespace CleenApi.Controllers
{
  public abstract class BaseCleenApiController<TEntity, TEntityChanges, TEntityQuery> : ApiController
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
    where TEntityQuery : class, IEntityQuery<TEntity>, new()
  {
    protected readonly CleenApiDbContext Db;

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected BaseCleenApiController()
    {
      Db = new CleenApiDbContext();
    }

    public TEntity Get(int id)
    {
      return GetById(id);
    }

    public TEntity[] Get()
    {
      IQueryable<TEntity> query = DbSet.AsQueryable();

      KeyValuePair<string, string>[] conditions = Request.GetQueryNameValuePairs().ToArray();
      if (conditions.Any())
      {
        query = new TEntityQuery().Build(query, conditions);
      }

      return query.ToArray();
    }

    public TEntity Post(TEntityChanges entityChanges)
    {
      if (entityChanges == null)
      {
        throw CreateInternalServerErrorException();
      }

      TEntity entity = entityChanges.Id.HasValue
                         ? DbSet.FirstOrDefault(e => e.Id == entityChanges.Id.Value)
                         : DbSet.Create();

      entity = entityChanges.ApplyValues(Db, entity);

      if (!entityChanges.IsValid(entity))
      {
        throw CreateInternalServerErrorException();
      }

      return Db.AddOrUpdate(entity);
    }

    public void Delete(int id)
    {
      try
      {
        DbSet.Remove(DbSet.Attach(new TEntity {Id = id}));
        Db.SaveChanges();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw CreateNotFoundException(id);
      }
    }

    protected override void Dispose(bool disposing)
    {
      Db.Dispose();
    }

    protected TEntity GetById(int id)
    {
      TEntity entity = DbSet.FirstOrDefault(e => e.Id == id);
      if (entity == null)
      {
        throw CreateNotFoundException(id);
      }

      return entity;
    }

    private static HttpException CreateNotFoundException(int id)
    {
      return new HttpException((int) HttpStatusCode.NotFound,
                               $"{typeof(TEntity).Name} with id {id} does not exist.");
    }

    private static HttpException CreateInternalServerErrorException()
    {
      return new HttpException((int) HttpStatusCode.InternalServerError,
                               $"The entity of type {typeof(TEntity).Name} is invalid.");
    }
  }
}