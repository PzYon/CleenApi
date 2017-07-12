using CleenApi.Library.EntitySets;

namespace CleenApi.WebApi.Entities.Locations
{
  public class LocationChanges : IEntityChanges<Location>
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public Location ApplyValues(Location location)
    {
      if (Name != null)
      {
        location.Name = Name;
      }

      return location;
    }

    public bool IsValidEntity(Location location)
    {
      return !string.IsNullOrEmpty(location.Name);
    }
  }
}