using CleenApi.Database;

namespace CleenApi.Entities.Implementations.Users
{
  public class UserChanges : IEntityChanges<User>
  {
    public int? Id { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }

    public User ApplyValues(CleenApiDbContext db, User user)
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

    public bool IsValid(User entity)
    {
      return !string.IsNullOrEmpty(entity.GivenName) && !string.IsNullOrEmpty(entity.Surname);
    }
  }
}