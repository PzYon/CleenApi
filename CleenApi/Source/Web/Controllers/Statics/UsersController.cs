using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Web.Controllers.Statics
{
  public class UsersController
    : BaseDbEntitySetController<User, UserEntitySet, UserChanges, UserQuery>
  {
  }
}