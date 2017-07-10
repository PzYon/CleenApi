using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.Library.Web.Controllers;
using CleenApi.WebApi.Database;

namespace CleenApi.WebApi.Entities.Locations
{
  public class LocationsController
    : BaseDbEntitySetController<
      CleenApiDbContext,
      Location,
      LocationEntitySet,
      LocationChanges,
      DbEntityQueryBuilder<Location>
    >
  {
  }
}