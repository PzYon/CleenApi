namespace CleenApi.Entities.Exceptions
{
  public interface IEntityNotFoundException
  {
  }

  public class EntityNotFoundException<TEntity> : BaseEntitySetException<TEntity>, IEntityNotFoundException
    where TEntity : IEntity
  {
    public EntityNotFoundException(int id) : base($"Entity with id {id} not found.")
    {
    }
  }
}