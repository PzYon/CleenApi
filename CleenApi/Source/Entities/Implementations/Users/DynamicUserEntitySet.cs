using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Implementations.Dynamics;

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
  }
}