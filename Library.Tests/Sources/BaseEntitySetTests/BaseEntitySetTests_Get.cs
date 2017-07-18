using System;
using System.Collections.Generic;
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
      var testEntitySet = new TestEntitySet(TestEntitiesRepo.DefaultEntities);

      Assert.AreEqual(testEntitySet.Get(1),
                      testEntitySet.RepoEntities.FirstOrDefault(e => e.Id == 1));
    }

    [TestMethod]
    [ExpectedException(typeof(EntityNotFoundException<TestEntity>))]
    public void ById_ThrowsNotFoundException()
    {
      new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(999);
    }

    [TestMethod]
    public void ByQuery_String_Contains()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), new EntityCondition(ConditionOperator.Contains, "a"));

      TestEntity[] entities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q).ToArray();

      Assert.IsTrue(entities.All(e => e.Name.Contains("a")));
    }

    [TestMethod]
    public void ByQuery_String_EndsWith()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), new EntityCondition(ConditionOperator.EndsWith, "r"));

      TestEntity[] entities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q).ToArray();

      Assert.IsTrue(entities.All(e => e.Name.EndsWith("r", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void ByQuery_String_BeginsWith()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), new EntityCondition(ConditionOperator.BeginsWith, "r"));

      TestEntity[] entities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q).ToArray();

      Assert.IsTrue(entities.All(e => e.Name.StartsWith("r", StringComparison.OrdinalIgnoreCase)));
    }

    [TestMethod]
    public void ByQuery_OrderByAscending()
    {
      var testEntitySet = new TestEntitySet(TestEntitiesRepo.DefaultEntities);

      var q = new TestEntitySetQuery();
      q.SortFields.Add(nameof(TestEntity.Name), SortDirection.Ascending);

      TestEntity[] entities = testEntitySet.Get(q).ToArray();

      TestEntity[] sorted = testEntitySet.RepoEntities.OrderBy(t => t.Name).ToArray();
      for (var i = 0; i < entities.Length; i++)
      {
        Assert.AreEqual(sorted[i], entities[i]);
      }
    }

    [TestMethod]
    public void ByQuery_OrderByDescending()
    {
      var testEntitySet = new TestEntitySet(TestEntitiesRepo.DefaultEntities);

      var q = new TestEntitySetQuery();
      q.SortFields.Add(nameof(TestEntity.Id), SortDirection.Descending);

      TestEntity[] entities = testEntitySet.Get(q).ToArray();

      TestEntity[] sorted = testEntitySet.RepoEntities.OrderByDescending(t => t.Id).ToArray();
      for (var i = 0; i < entities.Length; i++)
      {
        Assert.AreEqual(sorted[i], entities[i]);
      }
    }

    [TestMethod]
    public void ByQuery_ReturnsEmpty()
    {
      var q = new TestEntitySetQuery();
      q.Conditions.Add(nameof(TestEntity.Name), new EntityCondition(ConditionOperator.Equals, "MichGitt'sSicherNöd"));

      TestEntity[] entities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q).ToArray();

      Assert.AreEqual(0, entities.Length);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidRequestException))]
    public void Skip_NotSorted_Throws()
    {
      var q = new TestEntitySetQuery {Skip = 1};

      TestEntity[] entities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q).ToArray();
    }

    [TestMethod]
    public void Skip()
    {
      var testEntitySet = new TestEntitySet(TestEntitiesRepo.DefaultEntities);

      var q = new TestEntitySetQuery {Skip = 1};
      q.SortFields.Add(nameof(TestEntity.Name), SortDirection.Descending);

      TestEntity[] entities = testEntitySet.Get(q).ToArray();

      Assert.AreEqual(testEntitySet.RepoEntities.Count - 1, entities.Length);
    }

    [TestMethod]
    public void Take()
    {
      const int expected = 2;

      var q = new TestEntitySetQuery {Take = expected};
      q.SortFields.Add(nameof(TestEntity.Name), SortDirection.Descending);

      TestEntity[] entities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q).ToArray();

      Assert.AreEqual(expected, entities.Length);
    }

    [TestMethod]
    public void FullText()
    {
      var q = new TestEntitySetQuery {FullText = "roger"};

      IQueryable<TestEntity> testEntities = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Get(q);
      TestEntity[] entities = testEntities.ToArray();

      Assert.AreEqual(2, entities.Length);
    }

    [TestMethod]
    public void FullText_ConsidersExcludes()
    {
      var q = new TestEntitySetQuery {FullText = "roger"};

      var repoEntities = new List<TestEntity>
        {
          new TestEntity(12, "Foo", Stage.Alpha, "Bar", "bla bla roger bla bla"),
          new TestEntity(24, "Baz", Stage.Alpha, "Roger", "abc")
        };

      IQueryable<TestEntity> testEntities = new TestEntitySet(repoEntities).Get(q);
      TestEntity[] entities = testEntities.ToArray();

      Assert.AreEqual(1, entities.Length);
    }
  }
}