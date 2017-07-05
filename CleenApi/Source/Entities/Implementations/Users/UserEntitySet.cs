using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Implementations.Locations;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserEntitySet
    : DbEntitySet<User, UserChanges, EntityQueryBuilder<User>>
  {
    public UserEntitySet()
    {
    }

    public UserEntitySet(CleenApiDbContext db, IQueryable<User> queryable) : base(db, queryable)
    {
    }

    public LocationEntitySet GetLocationsSet(int userId)
    {
      IQueryable<Location> locations = GetByIdQuerable(userId).SelectMany(u => u.Locations);
      return new LocationEntitySet(Db, locations);
    }
  }
}