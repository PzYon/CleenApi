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
          GetAsKvp("containsCondition", EntitySetQuery.Chars.WildCard + "contains" + EntitySetQuery.Chars.WildCard),
          GetAsKvp("endsWithCondition", EntitySetQuery.Chars.WildCard + "endsWith"),
          GetAsKvp("beginsWithCondition", "beginsWith" + EntitySetQuery.Chars.WildCard),
          GetAsKvp("notEqualsCondition", EntitySetQuery.Chars.NotEqual + "not_equal"),
          GetAsKvp(EntitySetQuery.UrlKeys.Skip, "1"),
          GetAsKvp(EntitySetQuery.UrlKeys.Take, "2"),
          GetAsKvp(EntitySetQuery.UrlKeys.OrderBy,
                   EntitySetQuery.Chars.Descending + "Foo" + EntitySetQuery.Chars.Separator + "Bar"),
          GetAsKvp(EntitySetQuery.UrlKeys.Select, "Bli,Bla"),
          GetAsKvp(EntitySetQuery.UrlKeys.FullText, "miep")
        };

      var q = new EntitySetQuery(pairs);

      AssertCondition(q, "FirstCondition", "ConditionValue1", ConditionOperator.Equal);
      AssertCondition(q, "SecondCondition", "ConditionValue2", ConditionOperator.Equal);
      AssertCondition(q, "ContainsCondition", "contains", ConditionOperator.Contains);
      AssertCondition(q, "EndsWithCondition", "endsWith", ConditionOperator.EndsWith);
      AssertCondition(q, "BeginsWithCondition", "beginsWith", ConditionOperator.BeginsWith);
      AssertCondition(q, "NotEqualsCondition", "not_equal", ConditionOperator.NotEqual);

      Assert.AreEqual(1, q.Skip);
      Assert.AreEqual(2, q.Take);

      Assert.AreEqual(q.SortFields["Foo"], SortDirection.Descending);
      Assert.AreEqual(q.SortFields["Bar"], SortDirection.Ascending);

      Assert.IsTrue(q.Includes.Contains("Bli"));
      Assert.IsTrue(q.Includes.Contains("Bla"));

      Assert.AreEqual("miep", q.FullText);
    }

    private static void AssertCondition(IEntitySetQuery q,
                                        string propertyName,
                                        string expectedValue,
                                        ConditionOperator expectedOperator)
    {
      EntityCondition c = q.Conditions[propertyName];
      Assert.AreEqual(expectedValue, c.Value);
      Assert.AreEqual(expectedOperator, c.Operator);
    }

    private static KeyValuePair<string, string> GetAsKvp(string key, string value)
    {
      return new KeyValuePair<string, string>(key, value);
    }
  }
}