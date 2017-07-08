using CleenApi.Entities.Workspaces;

namespace CleenApi.Entities.NoDbItems
{
  public class NoDbItem : IEntity
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public bool IsValid { get; set; }

    public SomeType SomeType { get; set; }
  }
}