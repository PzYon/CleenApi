using System.Collections.Generic;
using CleenApi.Library.Queries;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntitySetQuery : IEntitySetQuery
  {
    public int Take { get; set; }

    public int Skip { get; set; }

    public Dictionary<string, string> Conditions { get; set; } = new Dictionary<string, string>();

    public string FullText { get; set; }

    public Dictionary<string, SortDirection> SortFields { get; set; } = new Dictionary<string, SortDirection>();

    public string[] Includes { get; set; } = new string[0];
  }
}