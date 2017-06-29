using System.Web.Http;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Controllers
{
  public class WorkspaceController : BaseCleenApiController<Workspace, WorkspaceChanges, WorkspaceQuery>
  {
    [HttpGet]
    [Route("workspace/test")]
    public string Test()
    {
      return "foo";
    }

    [HttpGet]
    [Route("workspace/{id}/likes")]
    public int GetLikes(int id)
    {
      return GetById(id).Likes;
    }

    [HttpPost]
    [Route("workspace/{id}/likes")]
    public int UpdateLikes(int id)
    {
      Workspace w = GetById(id);
      w.Likes++;

      Db.AddOrUpdate(w);

      return w.Likes;
    }
  }
}