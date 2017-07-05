using System.Linq;

namespace CleenApi.Entities.Queries.Builder
{
  public interface IEntityQueryBuilder<TEntity> where TEntity : IEntity
  {
    IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery entitySetQuery);

    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable, string[] propertiesToInclude);

    IQueryable<TEntity> ApplyDefaults(IQueryable<TEntity> queryable);
  }
}