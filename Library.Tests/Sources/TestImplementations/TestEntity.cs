using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntity : IEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public Stage Stage { get; set; }

    public TestEntity()
    {
    }

    public TestEntity(int id, string name, Stage stage)
    {
      Id = id;
      Name = name;
      Stage = stage;
    }
  }
}