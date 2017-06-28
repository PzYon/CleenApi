using System.Linq;
using CleenApi.Database;
using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceChanges : IEntityChanges<Workspace>
  {
    public int? Id { get; set; }

    public string Title { get; set; }

    public int[] UsersToAdd { get; set; } = new int[0];

    public Workspace ApplyValues(CleenApiDbContext db, Workspace workspace)
    {
      if (!string.IsNullOrEmpty(Title))
      {
        workspace.Title = Title;
      }

      if (UsersToAdd.Any())
      {
        foreach (int userId in UsersToAdd)
        {
          User user = db.Users.FirstOrDefault(u => u.Id == userId);
          if (user != null)
          {
            workspace.Users.Add(user);
          }
        }
      }

      return workspace;
    }

    public bool IsValid(Workspace entity)
    {
      return !string.IsNullOrEmpty(Title);
    }
  }
}