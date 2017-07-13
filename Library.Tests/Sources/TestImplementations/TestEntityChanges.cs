using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntityChanges : IEntityChanges<TestEntity>
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public Stage? Stage { get; set; }

    public TestEntity ApplyValues(TestEntity entity)
    {
      if (!string.IsNullOrEmpty(Name))
      {
        entity.Name = Name;
      }

      if (Stage.HasValue)
      {
        entity.Stage = Stage.Value;
      }

      return entity;
    }

    public bool IsValidEntity(TestEntity entity)
    {
      return !string.IsNullOrEmpty(entity.Name);
    }
  }
}