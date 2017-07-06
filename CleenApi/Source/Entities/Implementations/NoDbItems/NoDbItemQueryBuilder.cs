using System.Linq;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Entities.Implementations.NoDbItems
{
  public class NoDbItemQueryBuilder : EntityQueryBuilder<NoDbItem>
  {
    public override IQueryable<NoDbItem> ApplyDefaults(IQueryable<NoDbItem> queryable)
    {
      return queryable.Where(i => i.IsValid);
    }
  }
}