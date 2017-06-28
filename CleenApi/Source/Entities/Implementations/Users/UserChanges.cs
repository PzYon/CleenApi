namespace CleenApi.Entities.Implementations.Users
{
  public class UserChanges : IEntityChanges<User>
  {
    public int? Id { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }

    public User ApplyValues(User user)
    {
      if (!string.IsNullOrEmpty(GivenName))
      {
        user.GivenName = GivenName;
      }

      if (!string.IsNullOrEmpty(Surname))
      {
        user.Surname = Surname;
      }

      return user;
    }
  }
}