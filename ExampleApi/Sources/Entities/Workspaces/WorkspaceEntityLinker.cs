using System;
using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.Web.Hypermedia;

namespace CleenApi.ExampleApi.Entities.Workspaces
{
  public class WorkspaceEntityLinker : IEntityLinker
  {
    public List<Type> GetSupportedEntityTypes()
    {
      return new List<Type> {typeof(Workspace)};
    }

    public List<Link> GetLinks(object entity)
    {
      return GetLinksInternal(entity as Workspace).ToList();
    }

    private IEnumerable<Link> GetLinksInternal(Workspace workspace)
    {
      if (workspace == null)
      {
        yield break;
      }

      yield return new Link
        {
          Href = "/foo",
          Method = "POST",
          Rel = "foo-all"
        };

      yield return LinkUtility.BuildLink<WorkspacesController>(workspace.Id,
                                                                   "likes",
                                                                   nameof(WorkspacesController.GetLikes));

      yield return LinkUtility.BuildLink<WorkspacesController>(workspace.Id,
                                                                   "likes",
                                                                   nameof(WorkspacesController.UpdateLikes));

      yield return LinkUtility.BuildLink<WorkspacesController>(workspace.Id,
                                                                   "self",
                                                                   nameof(WorkspacesController.Get),
                                                                   new[] {typeof(int)});
    }
  }
}