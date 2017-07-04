using System.Linq;
using CleenApi.Database;

namespace CleenApi.Entities.Implementations.Locations
{
  public class DynamicLocationEntitySet : BaseDbEntitySet<Location, LocationChanges, LocationQuery>
  {
    public DynamicLocationEntitySet()
    {
    }

    public DynamicLocationEntitySet(CleenApiDbContext db, IQueryable<Location> queryable) : base(queryable)
    {
      SetDb(db);
    }
  }
}