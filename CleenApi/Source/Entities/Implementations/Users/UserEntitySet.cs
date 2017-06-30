﻿using System.Linq;
using CleenApi.Database;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserEntitySet : BaseDbEntitySet<User, UserChanges, UserQuery>
  {
    public UserEntitySet()
    {
    }

    public UserEntitySet(CleenApiDbContext db, IQueryable<User> users) : base(users)
    {
      SetDb(db);
    }
  }
}