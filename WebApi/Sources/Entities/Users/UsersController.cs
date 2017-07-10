using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.Library.Web.Controllers;
using CleenApi.WebApi.Database;

namespace CleenApi.WebApi.Entities.Users
{
  public class UsersController
    : BaseDbEntitySetController<
      CleenApiDbContext,
      User,
      UserEntitySet,
      UserChanges,
      DbEntityQueryBuilder<User>
    >
  {
  }
}