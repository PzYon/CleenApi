using System.Linq;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.ExampleApi.Entities.NoDbItems
{
  public class NoDbItemQueryBuilder : DefaultEntityQueryBuilder<NoDbItem>
  {
    public override IQueryable<NoDbItem> ApplyDefaults(IQueryable<NoDbItem> queryable)
    {
      return queryable.Where(i => i.IsValid);
    }
  }
}