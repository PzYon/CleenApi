using System.Linq;
using CleenApi.Queries.QueryBuilders;

namespace CleenApi.Entities.NoDbItems
{
  public class NoDbItemQueryBuilder : EntityQueryBuilder<NoDbItem>
  {
    public override IQueryable<NoDbItem> ApplyDefaults(IQueryable<NoDbItem> queryable)
    {
      return queryable.Where(i => i.IsValid);
    }
  }
}