using CleenApi.Library.EntitySets;

namespace CleenApi.WebApi.Entities.NoDbItems
{
  public class NoDbItemChanges : IEntityChanges<NoDbItem>
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public NoDbItem ApplyValues(NoDbItem noDbItem)
    {
      if (Title != null)
      {
        noDbItem.Title = Title;
      }

      return noDbItem;
    }

    public bool IsValidEntity(NoDbItem noDbItem)
    {
      return !string.IsNullOrEmpty(noDbItem.Title);
    }
  }
}