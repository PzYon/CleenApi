using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Web.Controllers.Statics
{
  public class UserController : BaseDbEntitySetController<User, UserEntitySet, UserChanges>
  {
  }
}