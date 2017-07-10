using System;
using System.Data.Entity;
using System.Linq;
using CleenApi.Library.Database;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.Library.EntitySets
{
  public class DbEntitySet<TDbContext, TEntity, TEntityChanges, TEntityQueryBuilder>
    : BaseEntitySet<TEntity, TEntityChanges, TEntityQueryBuilder>
    where TDbContext : BaseDbContext
    where TEntity : class, IEntity
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : DbEntityQueryBuilder<TEntity>, new()
  {
    protected TDbContext Db => GetDb();
    private TDbContext db;

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected override IQueryable<TEntity> Entities => entities ?? DbSet;
    private readonly IQueryable<TEntity> entities;

    public DbEntitySet()
    {
    }

    public DbEntitySet(TDbContext db, IQueryable<TEntity> entities)
    {
      this.db = db;
      this.entities = entities;
    }

    // hack: Db needs to be set via an interface method as generics don't support
    // new() constraint with constructor parameters.
    public void SetDb(TDbContext database)
    {
      if (db != null)
      {
        throw new ArgumentException("Cannot set DB as DB is already set.");
      }

      db = database;
    }

    private TDbContext GetDb()
    {
      if (db == null)
      {
        throw new Exception($"Database has not been set. Use {nameof(SetDb)}() to do so.");
      }

      return db;
    }

    protected override TEntity Store(TEntity entity)
    {
      return Db.AddOrUpdate(entity);
    }

    protected override TEntity Create()
    {
      return DbSet.Create();
    }

    protected override void Delete(TEntity entity)
    {
      DbSet.Remove(entity);
      Db.SaveChanges();
    }

    public override void Dispose()
    {
      Db.Dispose();
    }
  }
}