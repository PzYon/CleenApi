using System.Linq;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.WebApi.Database;
using CleenApi.WebApi.Entities.Locations;

namespace CleenApi.WebApi.Entities.Users
{
  public class UserEntitySet
    : DbEntitySet<CleenApiDbContext, User, UserChanges, DbEntityQueryBuilder<User>>
  {
    public UserEntitySet()
    {
    }

    public UserEntitySet(CleenApiDbContext db, IQueryable<User> queryable) : base(db, queryable)
    {
    }

    public LocationEntitySet GetLocationsSet(int userId)
    {
      IQueryable<Location> locations = GetByIdQueryable(userId).SelectMany(u => u.Locations);
      return new LocationEntitySet(Db, locations);
    }
  }
}