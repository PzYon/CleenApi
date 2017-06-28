﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserQuery : IEntityQuery<User>
  {
    public IQueryable<User> Build(IQueryable<User> set, KeyValuePair<string, string>[] conditions)
    {
      foreach (KeyValuePair<string, string> condition in conditions)
      {
        switch (condition.Key)
        {
          case nameof(User.Surname):
            set = set.Where(e => e.Surname == condition.Value);
            break;
          case nameof(User.GivenName):
            set = set.Where(e => e.GivenName == condition.Value);
            break;
          default:
            throw new ArgumentException($"Queries against '{condition.Key}' are not supported.");
        }
      }

      return set;
    }
  }
}