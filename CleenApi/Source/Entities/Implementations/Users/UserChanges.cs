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

    public bool IsValidEntity(User entity)
    {
      return !string.IsNullOrEmpty(entity.GivenName) && !string.IsNullOrEmpty(entity.Surname);
    }
  }
}