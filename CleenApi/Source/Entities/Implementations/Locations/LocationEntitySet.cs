using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Entities.Implementations.Locations
{
  public class LocationEntitySet : DbEntitySet<Location, LocationChanges, EntityQueryBuilder<Location>>
  {
    public LocationEntitySet()
    {
    }

    public LocationEntitySet(CleenApiDbContext db, IQueryable<Location> queryable) : base(db, queryable)
    {
    }
  }
}