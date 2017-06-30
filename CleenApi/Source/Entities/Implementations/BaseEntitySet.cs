using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Exceptions;

namespace CleenApi.Entities.Implementations
{
  public abstract class BaseEntitySet<TEntity, TEntityChanges, TEntityQuery> : IEntitySet<TEntity, TEntityChanges>
    where TEntity : class, IEntity, new()
    where TEntityChanges : class, IEntityChanges<TEntity>
    where TEntityQuery : class, IEntityQuery<TEntity>, new()
  {
    protected CleenApiDbContext Db { get; private set; }

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected IQueryable<TEntity> Entities => entities ?? DbSet;

    private readonly IQueryable<TEntity> entities;

    protected BaseEntitySet(IQueryable<TEntity> entities = null)
    {
      this.entities = entities;
    }

    public void SetDb(CleenApiDbContext db)
    {
      if (Db != null)
      {
        throw new ArgumentException("Cannot set DB as DB is already set.");
      }

      Db = db;
    }

    public TEntity Get(int id)
    {
      return GetById(id);
    }

    public IQueryable<TEntity> Get(KeyValuePair<string, string>[] conditions = null)
    {
      IQueryable<TEntity> query = Entities.AsQueryable();

      if (conditions?.Any() ?? false)
      {
        query = new TEntityQuery().Build(query, conditions);
      }

      return query;
    }

    public TEntity Update(TEntityChanges entityChanges)
    {
      if (entityChanges == null)
      {
        throw new EntityProcessingException<TEntity>("EntityChanges are null or cannot be parsed.");
      }

      bool isNew = entityChanges.Id.HasValue;

      TEntity entity = isNew
                         ? Entities.FirstOrDefault(e => e.Id == entityChanges.Id.Value)
                         : CreateNew();

      entity = entityChanges.ApplyValues(Db, entity);

      if (!entityChanges.IsValid(entity))
      {
        string message = isNew
                           ? $"Entity with id {entity.Id} is not valid after applying changes."
                           : "New entity is invalid";

        throw new EntityProcessingException<TEntity>(message);
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
        throw new EntityNotFoundException<TEntity>(id);
      }
    }

    public void Dispose()
    {
      Db.Dispose();
    }

    protected TEntity GetById(int id)
    {
      TEntity entity = Entities.FirstOrDefault(e => e.Id == id);
      if (entity == null)
      {
        throw new EntityNotFoundException<TEntity>(id);
      }

      return entity;
    }

    private TEntity CreateNew()
    {
      return DbSet.Create();
    }
  }
}