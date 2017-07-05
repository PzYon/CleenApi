using CleenApi.Entities.Implementations.Locations;

namespace CleenApi.Web.Controllers.Dynamics
{
  public class DynamicLocationsController
    : BaseDbEntitySetController<Location, DynamicLocationEntitySet, LocationChanges, LocationQuery>
  {
  }
}