namespace CleenApi.Entities.Exceptions
{
  public class InvalidSortFieldException<TEntity> : BaseEntitySetException<TEntity>
    where TEntity : IEntity
  {
    public InvalidSortFieldException(string fieldName)
      : base($"Field '{fieldName}' cannot be sorted.")
    {
    }
  }
}