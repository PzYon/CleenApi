namespace CleenApi.Library.EntitySets
{
  public interface IEntityChanges<TEntity> where TEntity : IEntity
  {
    int? Id { get; }

    TEntity ApplyValues(TEntity entity);

    bool IsValidEntity(TEntity entity);
  }
}