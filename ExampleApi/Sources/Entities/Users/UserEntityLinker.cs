using System;
using System.Collections.Generic;
using CleenApi.Library.Web.Hypermedia;

namespace CleenApi.ExampleApi.Entities.Users
{
  public class UserEntityLinker : IEntityLinker
  {
    public List<Type> GetSupportedEntityTypes()
    {
      return new List<Type> {typeof(User)};
    }

    public List<Link> GetLinks(object entity)
    {
      var u = entity as User;
      if (u == null)
      {
        return new List<Link>();
      }

      return new List<Link>
        {
          LinkUtility.BuildLink<UsersController>(u.Id, "foo", nameof(UsersController.Get))
        };
    }
  }
}