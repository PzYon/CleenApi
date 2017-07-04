﻿using System.Linq;
using System.Web.Http;
using CleenApi.Entities.Implementations.Users;
using CleenApi.Entities.Implementations.Workspaces;

namespace CleenApi.Web.Controllers.Statics
{
  public class WorkspacesController : BaseDbEntitySetController<Workspace, WorkspaceEntitySet, WorkspaceChanges>
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
                      .Get(EntitySetQuery)
                      .ToArray();
    }

    [HttpPost]
    [Route("workspaces/{workspaceId}/users")]
    public User AddUser(int workspaceId, [FromBody] UserChanges userChanges)
    {
      return EntitySet.AddUser(workspaceId, userChanges);
    }
  }
}