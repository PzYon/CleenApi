using System.Collections.Generic;

namespace CleenApi.Library.Queries
{
  public interface IEntitySetQuery
  {
    int Take { get; }

    int Skip { get; }

    Dictionary<string, string> Conditions { get; }

    string FullText { get; }

    Dictionary<string, SortDirection> SortFields { get; }

    string[] Includes { get; }
  }
}