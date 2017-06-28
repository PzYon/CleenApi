namespace CleenApi.Entities
{
  public interface IEntityChanges<TEntity> where TEntity : class, IEntity
  {
    int? Id { get; }

    TEntity ApplyValues(TEntity entity);
  }
}