using System.Linq;
using System.Web.Http;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Web.Controllers
{
  public class WorkspaceController : BaseEntitySetController<WorkspaceEntitySet, Workspace, WorkspaceChanges>
  {
    [HttpGet]
    [Route("workspace/{workspaceId}/likes")]
    public int GetLikes(int workspaceId)
    {
      return EntitySet.GetLikes(workspaceId);
    }

    [HttpPost]
    [Route("workspace/{workspaceId}/likes")]
    public int UpdateLikes(int workspaceId)
    {
      return EntitySet.UpdateLikes(workspaceId);
    }

    [HttpGet]
    [Route("workspace/{workspaceId}/users")]
    public User[] GetUsers(int workspaceId)
    {
      return EntitySet.GetUsersSet(workspaceId)
                      .Get(GetEntitySetQuery())
                      .ToArray();
    }
  }
}