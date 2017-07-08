using CleenApi.Entities;

namespace CleenApi.Exceptions
{
  public class InvalidConditionException<TEntity> : BaseEntitySetException<TEntity>, IInvalidRequestException
    where TEntity : IEntity
  {
    public InvalidConditionException(string fieldName, string value)
      : base($"Condition with value '{value}' against field {fieldName} are not allowed.")
    {
    }
  }
}