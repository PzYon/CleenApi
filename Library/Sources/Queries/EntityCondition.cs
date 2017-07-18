namespace CleenApi.Library.Queries
{
  public class EntityCondition
  {
    public ConditionOperator Operator { get; set; }

    public string Value { get; set; }

    public EntityCondition(ConditionOperator op, string value)
    {
      Operator = op;
      Value = value;
    }
  }
}