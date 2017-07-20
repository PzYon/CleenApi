using System.Data.Entity;
using System.Diagnostics;
using CleenApi.ExampleApi.Entities.Locations;
using CleenApi.ExampleApi.Entities.Users;
using CleenApi.ExampleApi.Entities.Workspaces;
using CleenApi.Library.Database;

namespace CleenApi.ExampleApi.Database
{
  public class CleenApiDbContext : BaseDbContext
  {
    public DbSet<Workspace> Workspaces { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Location> Locations { get; set; }

    public CleenApiDbContext() : base("CleenApi")
    {
      Database.Log = sql => Debug.Write(sql);
    }
  }
}