using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntity : IEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string AnotherDescription { get; set; }

    public Stage Stage { get; set; }

    public TestEntity()
    {
    }

    public TestEntity(int id, string name, Stage stage, string description = null, string anotherDescription = null)
    {
      Id = id;
      Name = name;
      Stage = stage;
      Description = description;
      AnotherDescription = anotherDescription;
    }
  }
}