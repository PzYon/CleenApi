using System.Linq;
using CleenApi.Library.EntitySets;
using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.WebApi.Database;
using CleenApi.WebApi.Entities.Locations;
using CleenApi.WebApi.Entities.Users;

namespace CleenApi.WebApi.Entities.Workspaces
{
  public class WorkspaceEntitySet
    : DbEntitySet<CleenApiDbContext, Workspace, WorkspaceChanges, DbEntityQueryBuilder<Workspace>>
  {
    public int GetLikes(int id)
    {
      return Get(id).Likes;
    }

    public int UpdateLikes(int id)
    {
      Workspace w = Get(id);
      w.Likes++;

      Db.AddOrUpdate(w);

      return w.Likes;
    }

    public User AddUser(int workspaceId, UserChanges userChanges)
    {
      User user = GetUsersSet(workspaceId).Create(userChanges);

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

    public LocationEntitySet GetLocationsSet(int workspaceId)
    {
      IQueryable<Location> locations = GetByIdQuerable(workspaceId).SelectMany(w => w.Locations);
      return new LocationEntitySet(Db, locations);
    }
  }
}