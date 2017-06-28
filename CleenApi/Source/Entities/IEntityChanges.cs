using CleenApi.Database;

namespace CleenApi.Entities
{
  public interface IEntityChanges<TEntity> where TEntity : class, IEntity
  {
    int? Id { get; }

    TEntity ApplyValues(CleenApiDbContext db, TEntity entity);

    bool IsValid(TEntity entity);
  }
}