using System.Collections.Generic;
using CleenApi.ExampleApi.Entities.Workspaces;

namespace CleenApi.ExampleApi.Entities.NoDbItems
{
  public static class NoDbItemsRepo
  {
    public static List<NoDbItem> Items = new List<NoDbItem>
      {
        new NoDbItem {Id = 1, Title = "Foo", IsValid = true},
        new NoDbItem {Id = 2, Title = "Bar", IsValid = true},
        new NoDbItem {Id = 3, Title = "Not Valid", SomeType = SomeType.SomethingElse},
        new NoDbItem {Id = 4, Title = "Valid", IsValid = true, SomeType = SomeType.SomethingElse}
      };
  }
}