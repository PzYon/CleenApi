using CleenApi.Database;

namespace CleenApi.Entities.Implementations.Locations
{
  public class LocationChanges : IEntityChanges<Location>
  {
    public int? Id { get; set; }

    public string Name { get; set; }

    public Location ApplyValues(CleenApiDbContext db, Location location)
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