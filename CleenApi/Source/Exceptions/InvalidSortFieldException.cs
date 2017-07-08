using CleenApi.Entities;

namespace CleenApi.Exceptions
{
  public class InvalidSortFieldException<TEntity> : BaseEntitySetException<TEntity>, IInvalidRequestException
    where TEntity : IEntity
  {
    public InvalidSortFieldException(string fieldName)
      : base($"Field '{fieldName}' cannot be sorted.")
    {
    }
  }
}