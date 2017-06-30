using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Web.Controllers
{
  public class UserController : BaseDbEntitySetController<UserEntitySet, User, UserChanges>
  {
  }
}