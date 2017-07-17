using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.Queries.LinqUtilities;
using CleenApi.Library.Queries.QueryBuilders;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntityQueryBuilder : BaseEntityQueryBuilder<TestEntity, DefaultLinqUtility>
  {
    internal const string NameToExclude = "exclude_me";

    public override IQueryable<TestEntity> ApplyDefaults(IQueryable<TestEntity> queryable)
    {
      return queryable.Where(e => e.Id != 666 && !e.Name.Contains(NameToExclude));
    }

    public override IEnumerable<string> GetPropertiesToExcludeFromFullText()
    {
      yield return nameof(TestEntity.AnotherDescription);
    }
  }
}