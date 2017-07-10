using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Exceptions
{
  public class InvalidEntityChangesException<TEntity> : EntityProcessingException<TEntity>
    where TEntity : IEntity
  {
    public InvalidEntityChangesException(int? id)
      : base(id.HasValue
               ? $"Entity with id {id} is not valid after applying changes."
               : "New entity is invalid")
    {
    }
  }
}