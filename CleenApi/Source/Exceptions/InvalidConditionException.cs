using CleenApi.Entities;

namespace CleenApi.Exceptions
{
  public class InvalidConditionException<TEntity> : BaseEntitySetException<TEntity>
    where TEntity : IEntity
  {
    public InvalidConditionException(string fieldName, string value)
      : base($"Condition with value '{value}' against field {fieldName} are not allowed.")
    {
    }
  }
}