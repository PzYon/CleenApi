namespace CleenApi.Entities.Exceptions
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