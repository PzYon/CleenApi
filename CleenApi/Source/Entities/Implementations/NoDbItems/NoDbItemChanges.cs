using CleenApi.Database;

namespace CleenApi.Entities.Implementations.NoDbItems
{
  public class NoDbItemChanges : IEntityChanges<NoDbItem>
  {
    public int? Id { get; }

    public string Title { get; set; }

    public NoDbItem ApplyValues(CleenApiDbContext db, NoDbItem noDbItem)
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