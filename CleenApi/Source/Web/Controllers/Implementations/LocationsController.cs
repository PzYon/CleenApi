using CleenApi.Entities.Locations;
using CleenApi.Queries.QueryBuilders;

namespace CleenApi.Web.Controllers.Implementations
{
  public class LocationsController
    : BaseDbEntitySetController<Location, LocationEntitySet, LocationChanges, DbEntityQueryBuilder<Location>>
  {
  }
}