using System.Collections.Generic;
using System.Linq;
using CleenApi.Entities.Exceptions;
using CleenApi.Entities.Queries;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserQuery : BaseEntityQuery<User>
  {
    protected override IQueryable<User> HandleConditions(IQueryable<User> set,
                                                         Dictionary<string, string> conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        string fieldName = condition.Key;
        string value = condition.Value;

        switch (fieldName)
        {
          case nameof(User.Surname):
            set = set.Where(e => e.Surname == value);
            break;

          case nameof(User.GivenName):
            set = set.Where(e => e.GivenName == value);
            break;

          default:
            throw new InvalidConditionException<User>(fieldName, value);
        }
      }

      return set;
    }

    protected override IQueryable<User> HandleOrderBy(IQueryable<User> set,
                                                      Dictionary<string, SortDirection> sortFields)
    {
      foreach (KeyValuePair<string, SortDirection> sortField in sortFields)
      {
        string fieldName = sortField.Key;

        switch (fieldName)
        {
          case nameof(User.GivenName):
            set = sortField.Value == SortDirection.Ascending
                    ? set.OrderBy(u => u.GivenName)
                    : set.OrderByDescending(u => u.GivenName);
            break;

          case nameof(User.Surname):
            set = sortField.Value == SortDirection.Ascending
                    ? set.OrderBy(u => u.Surname)
                    : set.OrderByDescending(u => u.Surname);
            break;

          default:
            throw new InvalidSortFieldException<User>(fieldName);
        }
      }

      return set;
    }

    protected override IQueryable<User> HandleIncludes(IQueryable<User> set, string[] propertiesToInclude)
    {
      return set;
    }
  }
}