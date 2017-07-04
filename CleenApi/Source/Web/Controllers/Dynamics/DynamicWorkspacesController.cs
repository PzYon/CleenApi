using System.Linq;
using System.Web.Http;
using CleenApi.Entities.Implementations.Locations;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Web.Controllers.Dynamics
{
  public class DynamicWorkspacesController
    : BaseDbEntitySetController<Workspace, DynamicWorkspaceEntitySet, WorkspaceChanges>
  {
    [HttpGet]
    [Route("dynamicworkspace/{workspaceId}/likes")]
    public int GetLikes(int workspaceId)
    {
      return EntitySet.GetLikes(workspaceId);
    }

    [HttpPost]
    [Route("dynamicworkspace/{workspaceId}/likes")]
    public int UpdateLikes(int workspaceId)
    {
      return EntitySet.UpdateLikes(workspaceId);
    }

    [HttpGet]
    [Route("dynamicworkspaces/{workspaceId}/users")]
    public User[] GetUsers(int workspaceId)
    {
      return EntitySet.GetUsersSet(workspaceId)
                      .Get(EntitySetQuery)
                      .ToArray();
    }

    [HttpPost]
    [Route("dynamicworkspaces/{workspaceId}/users")]
    public User AddUser(int workspaceId, [FromBody] UserChanges userChanges)
    {
      return EntitySet.AddUser(workspaceId, userChanges);
    }

    [HttpGet]
    [Route("dynamicworkspaces/{workspaceId}/locations")]
    public Location[] GetLocations(int workspaceId)
    {
      return EntitySet.GetLocationsSet(workspaceId)
                      .Get(EntitySetQuery)
                      .ToArray();
    }

    [HttpGet]
    [Route("dynamicworkspaces/{workspaceId}/users/{userId}/locations")]
    public Location[] GetLocationsFromUser(int workspaceId, int userId)
    {
      return EntitySet.GetUsersSet(workspaceId)
                      .GetLocationsSet(userId)
                      .Get(EntitySetQuery)
                      .ToArray();
    }
  }
}