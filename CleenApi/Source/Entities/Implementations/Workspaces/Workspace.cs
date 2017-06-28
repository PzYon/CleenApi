using System.Collections.Generic;
using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class Workspace : IEntity
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public List<User> Users { get; set; } = new List<User>();
    }
}