using CleenApi.Library.EntitySets;

namespace CleenApi.ExampleApi.Entities.Locations
{
  public class Location : IEntity
  {
    public int Id { get; set; }

    public string Name { get; set; }
  }
}