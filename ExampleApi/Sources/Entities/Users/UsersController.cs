using CleenApi.ExampleApi.Database;
using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.Library.Web.Controllers;

namespace CleenApi.ExampleApi.Entities.Users
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