namespace CleenApi.Entities.Implementations.Users
{
  public class User : IEntity
  {
    public int Id { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }
  }
}