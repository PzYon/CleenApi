using CleenApi.Entities;

namespace CleenApi.Exceptions
{
  public class EntityProcessingException<TEntity> : BaseEntitySetException<TEntity>, IEntityProcessingException
    where TEntity : IEntity
  {
    public EntityProcessingException(string message) : base($"Error processing entity: {message}")
    {
    }
  }
}