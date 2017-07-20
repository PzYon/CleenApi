using CleenApi.Library.EntitySets;

namespace CleenApi.ExampleApi.Entities.Users
{
  public class UserChanges : IEntityChanges<User>
  {
    public int Id { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }

    public User ApplyValues(User user)
    {
      if (GivenName != null)
      {
        user.GivenName = GivenName;
      }

      if (Surname != null)
      {
        user.Surname = Surname;
      }

      return user;
    }

    public bool IsValidEntity(User user)
    {
      return !string.IsNullOrEmpty(user.GivenName)
             && !string.IsNullOrEmpty(user.Surname);
    }
  }
}