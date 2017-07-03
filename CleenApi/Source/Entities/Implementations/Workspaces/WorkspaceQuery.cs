using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CleenApi.Entities.Exceptions;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceQuery : BaseEntityQuery<Workspace>
  {
    public override IQueryable<Workspace> ApplyDefaults(IQueryable<Workspace> queryable)
    {
      return queryable.Where(w => w.Id > 0);
    }

    public override IQueryable<Workspace> ApplyIncludes(IQueryable<Workspace> queryable,
                                                        string[] propertiesToInclude)
    {
      foreach (string propertyToInclude in propertiesToInclude)
      {
        switch (propertyToInclude)
        {
          case nameof(Workspace.Users):
            queryable = queryable.Include(s => s.Users);
            break;
        }
      }

      return queryable;
    }

    protected override IQueryable<Workspace> ApplyConditions(IQueryable<Workspace> queryable,
                                                             Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string fieldName = condition.Key;
        string value = condition.Value;

        switch (fieldName)
        {
          case nameof(Workspace.Title):
            queryable = queryable.Where(w => w.Title == value);
            break;

          default:
            throw new InvalidConditionException<Workspace>(fieldName, value);
        }
      }

      return queryable;
    }

    protected override IQueryable<Workspace> ApplyOrderBy(IQueryable<Workspace> queryable,
                                                          Dictionary<string, SortDirection> sortFields)
    {
      var isAlreadyOrdered = false;

      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string fieldName = sortField.Key;

        switch (fieldName)
        {
          case nameof(Workspace.Title):
            queryable = sortField.Value == SortDirection.Ascending
                          ? (isAlreadyOrdered
                               ? ((IOrderedQueryable<Workspace>) queryable).ThenBy(w => w.Title)
                               : queryable.OrderBy(w => w.Title))
                          : (isAlreadyOrdered
                               ? ((IOrderedQueryable<Workspace>) queryable).ThenByDescending(w => w.Title)
                               : queryable.OrderByDescending(w => w.Title));
            break;

          case nameof(Workspace.Likes):
            queryable = sortField.Value == SortDirection.Ascending
                          ? (isAlreadyOrdered
                               ? ((IOrderedQueryable<Workspace>) queryable).ThenBy(w => w.Likes)
                               : queryable.OrderBy(w => w.Likes))
                          : (isAlreadyOrdered
                               ? ((IOrderedQueryable<Workspace>) queryable).ThenByDescending(w => w.Likes)
                               : queryable.OrderByDescending(w => w.Likes));
            break;

          default:
            throw new InvalidSortFieldException<Workspace>(fieldName);
        }

        isAlreadyOrdered = true;
      }

      return queryable;
    }
  }
}