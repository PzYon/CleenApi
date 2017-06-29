using System.Web.Http;
using CleenApi.Controllers;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceController : BaseEntitySetController<WorkspaceEntitySet, Workspace, WorkspaceChanges>
  {
    [HttpGet]
    [Route("workspace/{id}/likes")]
    public int GetLikes(int id)
    {
      return Repo.GetLikes(id);
    }

    [HttpPost]
    [Route("workspace/{id}/likes")]
    public int UpdateLikes(int id)
    {
      return Repo.UpdateLikes(id);
    }
  }
}