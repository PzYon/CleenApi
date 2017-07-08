using System.Collections.Generic;
using CleenApi.Entities.Locations;
using CleenApi.Entities.Users;

namespace CleenApi.Entities.Workspaces
{
  public enum SomeType
  {
    Something,
    SomethingElse
  }

  public class Workspace : IEntity
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public int Likes { get; set; }

    public SomeType SomeType { get; set; }

    public virtual List<User> Users { get; set; } = new List<User>();

    public virtual List<Location> Locations { get; set; } = new List<Location>();
  }
}