using System.Linq;
using CleenApi.Queries.QueryBuilders;

namespace CleenApi.Entities.NoDbItems
{
  public class NoDbItemQueryBuilder : DefaultEntityQueryBuilder<NoDbItem>
  {
    public override IQueryable<NoDbItem> ApplyDefaults(IQueryable<NoDbItem> queryable)
    {
      return queryable.Where(i => i.IsValid);
    }
  }
}