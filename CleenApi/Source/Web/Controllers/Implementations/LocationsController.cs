using CleenApi.Entities.Implementations.Locations;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Web.Controllers.Implementations
{
  public class LocationsController
    : BaseDbEntitySetController<Location, LocationEntitySet, LocationChanges, EntityQueryBuilder<Location>>
  {
  }
}