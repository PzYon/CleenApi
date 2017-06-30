using System.Linq;

namespace CleenApi.Entities
{
  public interface IEntityQuery<TEntity> where TEntity : class, IEntity
  {
    IQueryable<TEntity> Build(IQueryable<TEntity> set, EntitySetQuery entitySetQuery);
  }
}