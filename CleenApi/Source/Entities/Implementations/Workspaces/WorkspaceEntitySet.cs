using System.Linq;
using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceEntitySet : BaseDbEntitySet<Workspace, WorkspaceChanges, WorkspaceQuery>
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

    public UserEntitySet GetUsersSet(int workspaceId)
    {
      IQueryable<User> users = GetByIdQuerable(workspaceId).SelectMany(w => w.Users);
      return new UserEntitySet(Db, users);
    }
  }
}