using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Implementations.Dynamics;
using CleenApi.Entities.Implementations.Locations;

namespace CleenApi.Entities.Implementations.Users
{
  public class DynamicUserEntitySet
    : BaseDbEntitySet<User, UserChanges, DynamicEntityQuery<User>>
  {
    public DynamicUserEntitySet()
    {
    }

    public DynamicUserEntitySet(CleenApiDbContext db, IQueryable<User> queryable) : base(queryable)
    {
      SetDb(db);
    }

    public DynamicLocationEntitySet GetLocationsSet(int userId)
    {
      // note: SelectMany is a hack to prevent an execution but still return only the users of the workspace
      return new DynamicLocationEntitySet(Db,
                                          Get().Where(u => u.Id == userId)
                                               .Take(1)
                                               .SelectMany(u => u.Locations));
    }
  }
}