using System;
using System.Data.Entity;
using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Entities.Implementations
{
  public class DbEntitySet<TEntity, TEntityChanges, TEntityQueryBuilder>
    : BaseEntitySet<TEntity, TEntityChanges, TEntityQueryBuilder>
    where TEntity : class, IEntity
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : class, IEntityQueryBuilder<TEntity>, new()
  {
    protected CleenApiDbContext Db => GetDb();
    private CleenApiDbContext db;

    protected DbSet<TEntity> DbSet => Db.Set<TEntity>();

    protected override IQueryable<TEntity> Entities => entities ?? DbSet;
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
      Db?.Dispose();
    }
  }
}