using System.Linq;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserEntitySet : BaseEntitySet<User, UserChanges, UserQuery>
  {
    

    public UserEntitySet()
    {
    }

    public UserEntitySet(IQueryable<User> users) : base(users)
    {
    }
  }
}