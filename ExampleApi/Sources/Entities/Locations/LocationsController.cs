using CleenApi.ExampleApi.Database;
using CleenApi.Library.Queries.QueryBuilders;
using CleenApi.Library.Web.Controllers;

namespace CleenApi.ExampleApi.Entities.Locations
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