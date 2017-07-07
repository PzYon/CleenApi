using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Web.Controllers.Implementations
{
  public class UsersController
    : BaseDbEntitySetController<User, UserEntitySet, UserChanges, DbEntityQueryBuilder<User>>
  {
  }
}