using System.Linq;
using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Queries.QueryBuilders
{
  public interface IEntityQueryBuilder<TEntity> where TEntity : IEntity
  {
    IQueryable<TEntity> Build(IQueryable<TEntity> queryable, EntitySetQuery entitySetQuery);

    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable, string[] propertiesToInclude);

    IQueryable<TEntity> ApplyDefaults(IQueryable<TEntity> queryable);
  }
}