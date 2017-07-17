using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Queries.QueryBuilders
{
  public interface IEntityQueryBuilder<TEntity> where TEntity : IEntity
  {
    IQueryable<TEntity> Build(IQueryable<TEntity> queryable, IEntitySetQuery entitySetQuery);

    IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable, string[] propertiesToInclude);

    IQueryable<TEntity> ApplyFullText(IQueryable<TEntity> queryable, string fullText);

    IQueryable<TEntity> ApplyDefaults(IQueryable<TEntity> queryable);

    IEnumerable<string> GetPropertiesToExcludeFromFullText();
  }
}