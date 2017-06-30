namespace CleenApi.Entities.Exceptions
{
  public interface IEntityProcessingException
  {
  }

  public class EntityProcessingException<TEntity> : BaseEntitySetException<TEntity>, IEntityProcessingException
    where TEntity : IEntity
  {
    public EntityProcessingException(string message) : base($"Error processing entity: {message}")
    {
    }
  }
}