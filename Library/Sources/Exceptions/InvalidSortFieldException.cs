using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Exceptions
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