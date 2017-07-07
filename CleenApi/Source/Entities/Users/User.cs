using System.Collections.Generic;
using CleenApi.Entities.Locations;

namespace CleenApi.Entities.Users
{
  public class User : IEntity
  {
    public int Id { get; set; }

    public string GivenName { get; set; }

    public string Surname { get; set; }

    public virtual List<Location> Locations { get; set; } = new List<Location>();
  }
}