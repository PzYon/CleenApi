using System.Collections.Generic;
using System.Data.Entity;
using CleenApi.WebApi.Entities.Locations;
using CleenApi.WebApi.Entities.Users;
using CleenApi.WebApi.Entities.Workspaces;

namespace CleenApi.WebApi.Database
{
  public class CleenApiDbInitializer : DropCreateDatabaseIfModelChanges<CleenApiDbContext>
  {
    protected override void Seed(CleenApiDbContext context)
    {
      context.Workspaces.Add(new Workspace
        {
          Title = "Soccer",
          Users = new List<User>
            {
              new User
                {
                  GivenName = "Cristiano",
                  Surname = "Ronaldo",
                  Locations = new List<Location>
                    {
                      new Location {Name = "Barcelona"},
                      new Location {Name = "Lisboa"}
                    }
                },
              new User
                {
                  GivenName = "Lionel",
                  Surname = "Messi",
                  Locations = new List<Location>
                    {
                      new Location {Name = "Barcelona"},
                      new Location {Name = "Buonas Aires"}
                    }
                }
            },
          Locations = new List<Location>
            {
              new Location {Name = "Worldwide"},
              new Location {Name = "Portugal"},
              new Location {Name = "Argentina"}
            },
          SomeType = SomeType.SomethingElse
        });

      context.Locations.Add(new Location {Name = "Basel"});
      context.Locations.Add(new Location {Name = "Zürich"});
      context.Locations.Add(new Location {Name = "Geneve"});
      context.Locations.Add(new Location {Name = "Luzern"});
      context.Locations.Add(new Location {Name = "Winterthur"});
      context.Locations.Add(new Location {Name = "Lugano"});
      context.Locations.Add(new Location {Name = "Beromünster"});

      context.Workspaces.Add(new Workspace {Title = "IT"});
      context.Workspaces.Add(new Workspace {Title = "Finance"});
      context.Workspaces.Add(new Workspace {Title = "Project", SomeType = SomeType.SomethingElse});
      context.Workspaces.Add(new Workspace {Title = "Corporate Fuckoff"});
      context.Workspaces.Add(new Workspace {Title = "Human Capital"});
      context.Workspaces.Add(new Workspace {Title = "Secret Stuff", SomeType = SomeType.SomethingElse});

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