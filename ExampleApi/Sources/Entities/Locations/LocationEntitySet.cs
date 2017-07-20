using System.Linq;
using CleenApi.ExampleApi.Database;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.ExampleApi.Entities.Locations
{
  public class LocationEntitySet
    : DbEntitySet<CleenApiDbContext, Location, LocationChanges, DbEntityQueryBuilder<Location>>
  {
    public LocationEntitySet()
    {
    }

    public LocationEntitySet(CleenApiDbContext db, IQueryable<Location> queryable) : base(db, queryable)
    {
    }
  }
}