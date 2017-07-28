using System.Collections.Generic;

namespace CleenApi.Library.Queries
{
  public interface IEntitySetQuery
  {
    int Take { get; }

    int Skip { get; }

    Dictionary<string, EntityCondition> Conditions { get; }

    string FullText { get; }

    Dictionary<string, SortDirection> SortFields { get; }

    string[] Expands { get; }

    string[] Selects { get; }
  }
}