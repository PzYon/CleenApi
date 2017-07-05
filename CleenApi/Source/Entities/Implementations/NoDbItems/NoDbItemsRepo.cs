using System.Collections.Generic;

namespace CleenApi.Entities.Implementations.NoDbItems
{
  public static class NoDbItemsRepo
  {
    public static List<NoDbItem> Items = new List<NoDbItem>
      {
        new NoDbItem {Id = 1, Title = "Foo"},
        new NoDbItem {Id = 1, Title = "Bar"}
      };
  }
}