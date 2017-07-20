using System.Linq;
using System.Web.Http;
using CleenApi.ExampleApi.Database;
using CleenApi.ExampleApi.Entities.Locations;
using CleenApi.ExampleApi.Entities.Users;
using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.Library.Web.Controllers;

namespace CleenApi.ExampleApi.Entities.Workspaces
{
  public class WorkspacesController
    : BaseDbEntitySetController<
      CleenApiDbContext,
      Workspace,
      WorkspaceEntitySet,
      WorkspaceChanges,
      DbEntityQueryBuilder<Workspace>
    >
  {
    [HttpGet]
    [Route("workspaces/{workspaceId}/likes")]
    public int GetLikes(int workspaceId)
    {
      return EntitySet.GetLikes(workspaceId);
    }

    [HttpPost]
    [Route("workspaces/{workspaceId}/likes")]
    public int UpdateLikes(int workspaceId)
    {
      return EntitySet.UpdateLikes(workspaceId);
    }

    [HttpGet]
    [Route("workspaces/{workspaceId}/users")]
    public User[] GetUsers(int workspaceId)
    {
      return EntitySet.GetUsersSet(workspaceId)
                      .Get(Query)
                      .ToArray();
    }

    [HttpPost]
    [Route("workspaces/{workspaceId}/users")]
    public User AddUser(int workspaceId, [FromBody] UserChanges userChanges)
    {
      return EntitySet.AddUser(workspaceId, userChanges);
    }

    [HttpGet]
    [Route("workspaces/{workspaceId}/locations")]
    public Location[] GetLocations(int workspaceId)
    {
      return EntitySet.GetLocationsSet(workspaceId)
                      .Get(Query)
                      .ToArray();
    }

    [HttpGet]
    [Route("workspaces/{workspaceId}/users/{userId}/locations")]
    public Location[] GetLocationsFromUser(int workspaceId, int userId)
    {
      return EntitySet.GetUsersSet(workspaceId)
                      .GetLocationsSet(userId)
                      .Get(Query)
                      .ToArray();
    }
  }
}