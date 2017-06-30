using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceQuery : BaseEntityQuery<Workspace>
  {
    protected override IQueryable<Workspace> HandleConditions(IQueryable<Workspace> set,
                                                              Dictionary<string, string> conditions)
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