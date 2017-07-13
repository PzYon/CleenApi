using System.Linq;
using CleenApi.Library.Exceptions;
using CleenApi.Library.Queries;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.Library.EntitySets
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

    public IQueryable<TEntity> Get(IEntitySetQuery query = null)
    {
      return QueryBuilder.Build(Entities, query);
    }

    public TEntity Create(TEntityChanges entityChanges)
    {
      if (entityChanges == null)
      {
        throw new EntityProcessingException<TEntity>("EntityChanges are null or cannot be parsed.");
      }

      if (entityChanges.Id > 0)
      {
        throw new EntityProcessingException<TEntity>("Cannot add an entity that already has an Id.");
      }

      return ApplyValuesAndPersist(entityChanges, Create());
    }

    public TEntity Update(TEntityChanges entityChanges, int id = 0)
    {
      if (entityChanges == null)
      {
        throw new EntityProcessingException<TEntity>("EntityChanges are null or cannot be parsed.");
      }

      int validId = EnsureValidId(id, entityChanges.Id);

      return ApplyValuesAndPersist(entityChanges, Get(validId));
    }

    public void Delete(int id)
    {
      Delete(Get(id));
    }

    protected IQueryable<TEntity> GetByIdQueryable(int id)
    {
      return Get().Where(e => e.Id == id).Take(1);
    }

    private TEntity ApplyValuesAndPersist(TEntityChanges entityChanges, TEntity entity)
    {
      entity = entityChanges.ApplyValues(entity);

      if (!entityChanges.IsValidEntity(entity))
      {
        throw new InvalidEntityChangesException<TEntity>(entityChanges.Id);
      }

      return Store(entity);
    }

    private static int EnsureValidId(int idFromUrl, int idFromChangesObject)
    {
      bool hasValueInChangesObject = idFromChangesObject > 0;
      bool hasValueInUrl = idFromUrl > 0;

      if (!hasValueInChangesObject && !hasValueInUrl)
      {
        throw new EntityProcessingException<TEntity>("An Id must be specified, either in "
                                                     + "the changes object or in the URL.");
      }

      if (hasValueInUrl && hasValueInChangesObject && idFromChangesObject != idFromUrl)
      {
        throw new EntityProcessingException<TEntity>($"Id in changes object ({idFromChangesObject}) "
                                                     + $"and Id in URL ({idFromUrl}) don't match.");
      }

      return hasValueInUrl
               ? idFromUrl
               : idFromChangesObject;
    }
  }
}