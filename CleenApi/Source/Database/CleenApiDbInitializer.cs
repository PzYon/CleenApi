using System.Collections.Generic;
using System.Data.Entity;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Database
{
  public class CleenApiDbInitializer : DropCreateDatabaseIfModelChanges<CleenApiDbContext>
  {
    protected override void Seed(CleenApiDbContext context)
    {
      context.Workspaces.Add(new Workspace {Title = "IT"});
      context.Workspaces.Add(new Workspace {Title = "Finance"});
      context.Workspaces.Add(new Workspace {Title = "Project"});
      context.Workspaces.Add(new Workspace {Title = "Corporate Fuckoff"});
      context.Workspaces.Add(new Workspace {Title = "Human Capital"});
      context.Workspaces.Add(new Workspace {Title = "Secret Stuff"});

      context.Workspaces.Add(new Workspace
        {
          Title = "Soccer",
          Users = new List<User>
            {
              new User
                {
                  GivenName = "Cristiano",
                  Surname = "Ronaldo"
                },
              new User
                {
                  GivenName = "Lionel",
                  Surname = "Messi"
                }
            }
        });

      context.Users.Add(new User {GivenName = "Markus", Surname = "Doggweiler"});
      context.Users.Add(new User {GivenName = "Patrick", Surname = "Doggweiler"});
      context.Users.Add(new User {GivenName = "Hans", Surname = "Doggweiler"});
      context.Users.Add(new User {GivenName = "Peter", Surname = "Sample"});
      context.Users.Add(new User {GivenName = "Eter", Surname = "Eger"});
      context.Users.Add(new User {GivenName = "Franz", Surname = "Klammer"});
      context.Users.Add(new User {GivenName = "Roger", Surname = "Federer"});
      context.Users.Add(new User {GivenName = "Stanis", Surname = "Las"});
      context.Users.Add(new User {GivenName = "Franz", Surname = "Klammer"});
    }
  }
}