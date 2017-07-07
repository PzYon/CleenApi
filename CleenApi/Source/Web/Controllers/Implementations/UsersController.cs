using CleenApi.Entities.Users;
using CleenApi.Queries.QueryBuilders;

namespace CleenApi.Web.Controllers.Implementations
{
  public class UsersController
    : BaseDbEntitySetController<User, UserEntitySet, UserChanges, DbEntityQueryBuilder<User>>
  {
  }
}