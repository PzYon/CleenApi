using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Web.Controllers
{
  public class UserController : BaseEntitySetController<UserEntitySet, User, UserChanges>
  {
  }
}