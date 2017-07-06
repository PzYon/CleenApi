using System.Linq;
using CleenApi.Entities.Queries;
using CleenApi.Entities.Queries.Builder;
using CleenApi.Exceptions;

namespace CleenApi.Entities.Implementations
{
  public abstract class BaseEntitySet<TEntity, TEntityChanges, TEntityQueryBuilder>
    : IEntitySet<TEntity, TEntityChanges>
    where TEntity : class, IEntity
    where TEntityChanges : IEntityChanges<TEntity>
    where TEntityQueryBuilder : class, IEntityQueryBuilder<TEntity>, new()
  {
    protected readonly TEntityQueryBuilder QueryBuilder = new TEntityQueryBuilder();

    protected abstract IQueryable<TEntity> Entities { get; }

    protected abstract TEntity Create();
    protected abstract TEntity Store(TEntity entity);
    protected abstract void Delete(TEntity entity);

    public abstract void Dispose();

    public TEntity Get(int id, string[] includes = null)
    {
      IQueryable<TEntity> queryable = QueryBuilder.ApplyDefaults(Entities);

      if (includes != null)
      {
        queryable = QueryBuilder.ApplyIncludes(queryable, includes);
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
      return QueryBuilder.Build(Entities, query);
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
                         : Create();

      entity = entityChanges.ApplyValues(entity);

      if (!entityChanges.IsValidEntity(entity))
      {
        throw new InvalidEntityChangesException<TEntity>(entityChanges.Id);
      }

      return Store(entity);
    }

    public void Delete(int id)
    {
      Delete(Get(id));
    }

    protected IQueryable<TEntity> GetByIdQuerable(int id)
    {
      return Get().Where(e => e.Id == id).Take(1);
    }
  }
}