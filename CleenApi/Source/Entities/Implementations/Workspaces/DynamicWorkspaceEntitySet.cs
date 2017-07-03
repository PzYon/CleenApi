using System.Linq;
using CleenApi.Entities.Implementations.Dynamics;
using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class DynamicWorkspaceEntitySet : BaseDbEntitySet<Workspace, WorkspaceChanges, DynamicEntityQuery<Workspace>>
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

    public User AddUser(int workspaceId, UserChanges userChanges)
    {
      User user = GetUsersSet(workspaceId).Update(userChanges);

      Workspace workspace = Get(workspaceId);
      workspace.Users.Add(user);

      Db.SaveChanges();

      return user;
    }

    public DynamicUserEntitySet GetUsersSet(int workspaceId)
    {
      // note: SelectMany is a hack to prevent an execution but still return only the users of the workspace
      return new DynamicUserEntitySet(Db,
                                      Db.Workspaces
                                        .Where(w => w.Id == workspaceId)
                                        .Take(1)
                                        .SelectMany(w => w.Users));
    }
  }
}