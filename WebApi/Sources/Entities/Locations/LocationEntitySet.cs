using System.Linq;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.WebApi.Database;

namespace CleenApi.WebApi.Entities.Locations
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