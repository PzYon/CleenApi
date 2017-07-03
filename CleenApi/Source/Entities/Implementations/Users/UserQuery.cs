using System.Collections.Generic;
using System.Linq;
using CleenApi.Entities.Exceptions;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserQuery : BaseEntityQuery<User>
  {
    protected override IQueryable<User> HandleConditions(IQueryable<User> queryable,
                                                         Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string fieldName = condition.Key;
        string value = condition.Value;

        switch (fieldName)
        {
          case nameof(User.Surname):
            queryable = queryable.Where(e => e.Surname == value);
            break;

          case nameof(User.GivenName):
            queryable = queryable.Where(e => e.GivenName == value);
            break;

          default:
            throw new InvalidConditionException<User>(fieldName, value);
        }
      }

      return queryable;
    }

    protected override IQueryable<User> HandleOrderBy(IQueryable<User> queryable,
                                                      Dictionary<string, SortDirection> sortFields)
    {
      var isAlreadyOrdered = false;

      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string fieldName = sortField.Key;

        switch (fieldName)
        {
          case nameof(User.GivenName):
            queryable = sortField.Value == SortDirection.Ascending
                          ? (isAlreadyOrdered
                               ? ((IOrderedQueryable<User>) queryable).ThenBy(w => w.GivenName)
                               : queryable.OrderBy(w => w.GivenName))
                          : (isAlreadyOrdered
                               ? ((IOrderedQueryable<User>) queryable).ThenByDescending(w => w.GivenName)
                               : queryable.OrderByDescending(w => w.GivenName));
            break;

          case nameof(User.Surname):
            queryable = sortField.Value == SortDirection.Ascending
                          ? (isAlreadyOrdered
                               ? ((IOrderedQueryable<User>) queryable).ThenBy(w => w.Surname)
                               : queryable.OrderBy(w => w.Surname))
                          : (isAlreadyOrdered
                               ? ((IOrderedQueryable<User>) queryable).ThenByDescending(w => w.Surname)
                               : queryable.OrderByDescending(w => w.Surname));
            break;

          default:
            throw new InvalidSortFieldException<User>(fieldName);
        }

        isAlreadyOrdered = true;
      }

      return queryable;
    }

    protected override IQueryable<User> HandleIncludes(IQueryable<User> queryable, string[] propertiesToInclude)
    {
      return queryable;
    }
  }
}