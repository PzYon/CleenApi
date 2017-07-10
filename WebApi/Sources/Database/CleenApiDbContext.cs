using System.Data.Entity;
using System.Diagnostics;
using CleenApi.Library.Database;
using CleenApi.WebApi.Entities.Locations;
using CleenApi.WebApi.Entities.Users;
using CleenApi.WebApi.Entities.Workspaces;

namespace CleenApi.WebApi.Database
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