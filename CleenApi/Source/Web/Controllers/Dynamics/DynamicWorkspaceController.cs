using System.Linq;
using System.Web.Http;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Web.Controllers.Dynamics
{
  public class DynamicWorkspaceController
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
    [Route("dynamicworkspace/{workspaceId}/users")]
    public User[] GetUsers(int workspaceId)
    {
      return EntitySet.GetUsersSet(workspaceId)
                      .Get(EntitySetQuery)
                      .ToArray();
    }

    [HttpPost]
    [Route("dynamicworkspace/{workspaceId}/users")]
    public User AddUser(int workspaceId, [FromBody] UserChanges userChanges)
    {
      return EntitySet.AddUser(workspaceId, userChanges);
    }
  }
}