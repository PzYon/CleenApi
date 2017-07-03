using System.Linq;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities
{
  public interface IEntityQuery<TEntity> where TEntity : class, IEntity
  {
    IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery entitySetQuery);

    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable, string[] propertiesToInclude);

    IQueryable<TEntity> ApplyDefaults(IQueryable<TEntity> queryable);
  }
}