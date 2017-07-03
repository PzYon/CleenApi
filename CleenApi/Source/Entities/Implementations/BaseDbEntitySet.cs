using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Exceptions;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations
{
  public abstract class BaseDbEntitySet<TEntity, TEntityChanges, TEntityQuery> : IEntitySet<TEntity, TEntityChanges>
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
    where TEntityQuery : class, IEntityQuery<TEntity>, new()
  {
    protected CleenApiDbContext Db { get; private set; }

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected IQueryable<TEntity> Entities => entities ?? DbSet;

    private readonly IQueryable<TEntity> entities;

    protected BaseDbEntitySet()
    {
    }

    protected BaseDbEntitySet(IQueryable<TEntity> entities)
    {
      this.entities = entities;
    }

    // hack: Db needs to be set via an interface method as generics don't support
    // new() constraint with constructor parameters.
    public void SetDb(CleenApiDbContext db)
    {
      if (Db != null)
      {
        throw new ArgumentException("Cannot set DB as DB is already set.");
      }

      Db = db;
    }

    public TEntity Get(int id, string[] includes = null)
    {
      return GetById(id, includes);
    }

    public IQueryable<TEntity> Get(EntitySetQuery query = null)
    {
      IQueryable<TEntity> queryable = Entities.AsQueryable();

      return query != null
               ? new TEntityQuery().Build(queryable, query)
               : queryable;
    }

    public TEntity Update(TEntityChanges entityChanges)
    {
      if (entityChanges == null)
      {
        throw new EntityProcessingException<TEntity>("EntityChanges are null or cannot be parsed.");
      }

      bool isNew = entityChanges.Id.HasValue && entityChanges.Id.Value > 0;

      TEntity entity = isNew
                         ? GetById(entityChanges.Id.Value)
                         : DbSet.Create();

      entity = entityChanges.ApplyValues(Db, entity);

      if (entityChanges.IsValidEntity(entity))
      {
        return Db.AddOrUpdate(entity);
      }

      throw new InvalidEntityChangesException<TEntity>(entityChanges.Id);
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
        throw new EntityNotFoundException<TEntity>(id);
      }
    }

    public void Dispose()
    {
      Db.Dispose();
    }

    protected TEntity GetById(int id, string[] includes = null)
    {
      IQueryable<TEntity> queryable = Entities;

      if (includes != null)
      {
        foreach (string include in includes)
        {
          queryable = queryable.Include(include);
        }
      }

      TEntity entity = queryable.FirstOrDefault(e => e.Id == id);
      if (entity == null)
      {
        throw new EntityNotFoundException<TEntity>(id);
      }

      return entity;
    }
  }
}