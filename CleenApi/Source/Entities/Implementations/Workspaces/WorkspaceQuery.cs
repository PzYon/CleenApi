using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleenApi.Entities.Exceptions;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceQuery : BaseEntityQuery<Workspace>
  {
    protected override IQueryable<Workspace> HandleConditions(IQueryable<Workspace> set,
                                                              Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string fieldName = condition.Key;
        string value = condition.Value;

        switch (fieldName)
        {
          case nameof(Workspace.Title):
            set = set.Where(w => w.Title == value);
            break;

          default:
            throw new InvalidConditionException<Workspace>(fieldName, value);
        }
      }

      return set;
    }

    protected override IQueryable<Workspace> HandleOrderBy(IQueryable<Workspace> set,
                                                           Dictionary<string, SortDirection> sortFields)
    {
      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string fieldName = sortField.Key;

        switch (fieldName)
        {
          case nameof(Workspace.Title):
            set = sortField.Value == SortDirection.Ascending
                    ? set.OrderBy(w => w.Title)
                    : set.OrderByDescending(w => w.Title);
            break;

          case nameof(Workspace.Likes):
            set = sortField.Value == SortDirection.Ascending
                    ? set.OrderBy(w => w.Likes)
                    : set.OrderByDescending(w => w.Likes);
            break;

          default:
            throw new InvalidSortFieldException<Workspace>(fieldName);
        }
      }

      return set;
    }

    protected override IQueryable<Workspace> HandleIncludes(IQueryable<Workspace> set, string[] propertiesToInclude)
    {
      foreach (string propertyToInclude in propertiesToInclude)
      {
        switch (propertyToInclude)
        {
          case nameof(Workspace.Users):
            set = set.Include(s => s.Users);
            break;
        }
      }

      return set;
    }
  }
}