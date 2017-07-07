using System.Linq;
using CleenApi.Database;
using CleenApi.Queries.QueryBuilders;

namespace CleenApi.Entities.Locations
{
  public class LocationEntitySet : DbEntitySet<Location, LocationChanges, DbEntityQueryBuilder<Location>>
  {
    public LocationEntitySet()
    {
    }

    public LocationEntitySet(CleenApiDbContext db, IQueryable<Location> queryable) : base(db, queryable)
    {
    }
  }
}