using System;
using System.Data.Entity;
using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Queries;
using CleenApi.Entities.Queries.Builder;
using CleenApi.Exceptions;

namespace CleenApi.Entities.Implementations
{
  public class DbEntitySet<TEntity, TEntityChanges, TEntityQueryBuilder> : IEntitySet<TEntity, TEntityChanges>
    where TEntity : class, IEntity
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : class, IEntityQueryBuilder<TEntity>, new()
  {
    protected CleenApiDbContext Db => GetDb();
    private CleenApiDbContext db;

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected TEntityQueryBuilder EntityQuery => new TEntityQueryBuilder();

    protected IQueryable<TEntity> Entities => entities ?? DbSet;
    private readonly IQueryable<TEntity> entities;

    public DbEntitySet()
    {
    }

    public DbEntitySet(CleenApiDbContext db, IQueryable<TEntity> entities)
    {
      this.db = db;
      this.entities = entities;
    }

    // hack: Db needs to be set via an interface method as generics don't support
    // new() constraint with constructor parameters.
    public void SetDb(CleenApiDbContext database)
    {
      if (db != null)
      {
        throw new ArgumentException("Cannot set DB as DB is already set.");
      }

      db = database;
    }

    private CleenApiDbContext GetDb()
    {
      if (db == null)
      {
        throw new Exception($"Database has not been set. Use {nameof(SetDb)}() to do so.");
      }

      return db;
    }

    public TEntity Get(int id, string[] includes = null)
    {
      IQueryable<TEntity> queryable = EntityQuery.ApplyDefaults(Entities);

      if (includes != null)
      {
        queryable = EntityQuery.ApplyIncludes(queryable, includes);
      }

      TEntity entity = queryable.SingleOrDefault(e => e.Id == id);
      if (entity == null)
      {
        throw new EntityNotFoundException<TEntity>(id);
      }

      return entity;
    }

    public IQueryable<TEntity> Get(EntitySetQuery query = null)
    {
      IQueryable<TEntity> queryable = Entities;

      return query != null
               ? EntityQuery.Build(queryable, query)
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
                         ? Get(entityChanges.Id.Value)
                         : DbSet.Create();

      entity = entityChanges.ApplyValues(Db, entity);

      if (!entityChanges.IsValidEntity(entity))
      {
        throw new InvalidEntityChangesException<TEntity>(entityChanges.Id);
      }

      return Db.AddOrUpdate(entity);
    }

    public void Delete(int id)
    {
      DbSet.Remove(Get(id));
      Db.SaveChanges();
    }

    public void Dispose()
    {
      Db?.Dispose();
    }

    protected IQueryable<TEntity> GetByIdQuerable(int id)
    {
      return Get().Where(e => e.Id == id).Take(1);
    }
  }
}