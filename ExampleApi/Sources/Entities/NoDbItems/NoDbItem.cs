using CleenApi.ExampleApi.Entities.Workspaces;
using CleenApi.Library.EntitySets;

namespace CleenApi.ExampleApi.Entities.NoDbItems
{
  public class NoDbItem : IEntity
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public bool IsValid { get; set; }

    public SomeType SomeType { get; set; }
  }
}