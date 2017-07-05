﻿using CleenApi.Entities.Implementations.Dynamics;
using CleenApi.Entities.Implementations.Users;

namespace CleenApi.Web.Controllers.Dynamics
{
  public class DynamicUsersController
    : BaseDbEntitySetController<User, DynamicUserEntitySet, UserChanges, DynamicEntityQuery<User>>
  {
  }
}