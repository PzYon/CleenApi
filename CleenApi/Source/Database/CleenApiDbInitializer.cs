using System.Data.Entity;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Database
{
  public class CleenApiDbInitializer : DropCreateDatabaseIfModelChanges<CleenApiDbContext>
  {
    protected override void Seed(CleenApiDbContext context)
    {
      context.Workspaces.Add(new Workspace {Title = "Foo"});
      context.Workspaces.Add(new Workspace {Title = "Bar"});

      context.Users.Add(new User {GivenName = "Markus", Surname = "Doggweiler"});
      context.Users.Add(new User {GivenName = "Peter", Surname = "Sample"});
    }
  }
}