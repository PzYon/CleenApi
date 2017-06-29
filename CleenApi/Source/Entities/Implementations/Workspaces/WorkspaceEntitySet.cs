using System.Linq;
using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceEntitySet : BaseEntitySet<Workspace, WorkspaceChanges, WorkspaceQuery>
  {
    public int GetLikes(int id)
    {
      return GetById(id).Likes;
    }

    public int UpdateLikes(int id)
    {
      Workspace w = GetById(id);
      w.Likes++;

      Db.AddOrUpdate(w);

      return w.Likes;
    }

    public UserEntitySet GetUsersSet(int workspaceId)
    {
      // note: SelectMany is a hack to prevent an execution but still return only the users of the workspace
      return new UserEntitySet(Db,
                               Db.Workspaces
                                 .Where(w => w.Id == workspaceId)
                                 .Take(1)
                                 .SelectMany(w => w.Users));
    }
  }
}