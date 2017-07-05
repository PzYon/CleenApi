using CleenApi.Database;

namespace CleenApi.Entities
{
  public interface IEntityChanges<TEntity> where TEntity : IEntity
  {
    int? Id { get; }

    TEntity ApplyValues(CleenApiDbContext db, TEntity entity);

    bool IsValidEntity(TEntity entity);
  }
}