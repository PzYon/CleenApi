using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Web.Controllers.Dynamics
{
  public class DynamicUserController
    : BaseDbEntitySetController<User, DynamicUserEntitySet, UserChanges>
  {
  }
}