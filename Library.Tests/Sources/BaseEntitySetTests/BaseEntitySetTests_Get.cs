using System;
using System.Linq;
using CleenApi.Library.Exceptions;
using CleenApi.Library.Queries;
using CleenApi.Library.Tests.TestImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleenApi.Library.Tests.BaseEntitySetTests
{
  // todo:
  // - combined conditions
  // - includes

  [TestClass]
  public class BaseEntitySetTests_Get
  {
    [TestMethod]
    public void ById()
    {
      Assert.AreEqual(new TestEntitySet().Get(1),
                      TestEntitiesRepo.Entities.FirstOrDefault(e => e.Id == 1));
    }

    [TestMethod]
    [ExpectedException(typeof(EntityNotFoundException<TestEntity>))]
    public void ById_ThrowsNotFoundException()
    {
      new TestEntitySet().Get(999);
    }

    [TestMethod]
    public void ByQuery_String_Contains()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), "*a*");

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      Assert.IsTrue(entities.All(e => e.Name.Contains("a")));
    }

    [TestMethod]
    public void ByQuery_String_EndsWith()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), "r");

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      Assert.IsTrue(entities.All(e => e.Name.EndsWith("r", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void ByQuery_String_StartsWith()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), "r*");

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      Assert.IsTrue(entities.All(e => e.Name.StartsWith("r", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void ByQuery_OrderByAscending()
    {
      var q = new TestEntitySetQuery();
      q.SortFields.Add(nameof(TestEntity.Name), SortDirection.Ascending);

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      TestEntity[] sorted = TestEntitiesRepo.Entities.OrderBy(t => t.Name).ToArray();
      for (var i = 0; i < entities.Length; i++)
      {
        Assert.AreEqual(sorted[i], entities[i]);
      }
    }

    [TestMethod]
    public void ByQuery_OrderByDescending()
    {
      var q = new TestEntitySetQuery();
      q.SortFields.Add(nameof(TestEntity.Id), SortDirection.Descending);

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      TestEntity[] sorted = TestEntitiesRepo.Entities.OrderByDescending(t => t.Id).ToArray();
      for (var i = 0; i < entities.Length; i++)
      {
        Assert.AreEqual(sorted[i], entities[i]);
      }
    }

    [TestMethod]
    public void ByQuery_ReturnsEmpty()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), "MichGitt'sSicherNöd");

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      Assert.AreEqual(0, entities.Length);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidRequestException))]
    public void Skip_NotSorted_Throws()
    {
      var q = new TestEntitySetQuery {Skip = 1};

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();
    }

    [TestMethod]
    public void Skip()
    {
      var q = new TestEntitySetQuery {Skip = 1};
      q.SortFields.Add(nameof(TestEntity.Name), SortDirection.Descending);

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      Assert.AreEqual(TestEntitiesRepo.Entities.Count - 1, entities.Length);
    }

    [TestMethod]
    public void Take()
    {
      var expected = 2;

      var q = new TestEntitySetQuery {Take = expected};
      q.SortFields.Add(nameof(TestEntity.Name), SortDirection.Descending);

      TestEntity[] entities = new TestEntitySet().Get(q).ToArray();

      Assert.AreEqual(expected, entities.Length);
    }
  }
}