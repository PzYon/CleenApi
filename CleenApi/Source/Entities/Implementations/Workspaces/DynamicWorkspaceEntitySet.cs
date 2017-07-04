using System.Linq;
using CleenApi.Entities.Implementations.Dynamics;
using CleenApi.Entities.Implementations.Locations;
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
      IQueryable<User> users = GetByIdQuerable(workspaceId).SelectMany(w => w.Users);
      return new DynamicUserEntitySet(Db, users);
    }

    public DynamicLocationEntitySet GetLocationsSet(int workspaceId)
    {
      IQueryable<Location> locations = GetByIdQuerable(workspaceId).SelectMany(w => w.Locations);
      return new DynamicLocationEntitySet(Db, locations);
    }
  }
}