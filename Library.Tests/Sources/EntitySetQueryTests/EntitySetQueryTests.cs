using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleenApi.Library.Tests.EntitySetQueryTests
{
  [TestClass]
  public class EntitySetQueryTests
  {
    [TestMethod]
    public void Pairs_AreParsedCorrectly()
    {
      var pairs = new[]
        {
          GetAsKvp("FirstCondition", "ConditionValue1"),
          GetAsKvp("secondCondition", "ConditionValue2"),
          GetAsKvp(EntitySetQuery.UrlKeys.Skip, "1"),
          GetAsKvp(EntitySetQuery.UrlKeys.Take, "2"),
          GetAsKvp(EntitySetQuery.UrlKeys.OrderBy, "-Foo,Bar"),
          GetAsKvp(EntitySetQuery.UrlKeys.Select, "Bli,Bla")
        };

      var q = new EntitySetQuery(pairs);

      Assert.AreEqual("ConditionValue1", q.Conditions["FirstCondition"]);
      Assert.AreEqual("ConditionValue2", q.Conditions["SecondCondition"]);

      Assert.AreEqual(1, q.Skip);
      Assert.AreEqual(2, q.Take);

      Assert.AreEqual(q.SortFields["Foo"], SortDirection.Descending);
      Assert.AreEqual(q.SortFields["Bar"], SortDirection.Ascending);

      Assert.IsTrue(q.Includes.Contains("Bli"));
      Assert.IsTrue(q.Includes.Contains("Bla"));
    }

    private KeyValuePair<string, string> GetAsKvp(string key, string value)
    {
      return new KeyValuePair<string, string>(key, value);
    }
  }
}