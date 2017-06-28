using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceQuery : IEntityQuery<Workspace>
  {
    public IQueryable<Workspace> Build(IQueryable<Workspace> set, KeyValuePair<string, string>[] conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        switch (condition.Key)
        {
          case nameof(Workspace.Title):
            set = set.Where(e => e.Title == condition.Value);
            break;
          default:
            throw new ArgumentException($"Queries against '{condition.Key}' are not supported.");
        }
      }

      return set;
    }
  }
}