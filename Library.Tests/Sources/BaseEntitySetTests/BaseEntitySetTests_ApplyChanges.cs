using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.Exceptions;
using CleenApi.Library.Tests.TestImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleenApi.Library.Tests.BaseEntitySetTests
{
  [TestClass]
  public class BaseEntitySetTests_ApplyChanges
  {
    [TestMethod]
    [ExpectedException(typeof(EntityNotFoundException<TestEntity>))]
    public void ApplyDefaults_GetById()
    {
      var testEntities = new List<TestEntity>
        {
          new TestEntity(1, TestEntityQueryBuilder.NameToExclude, Stage.Alpha)
        };

      TestEntity entity = new TestEntitySet(testEntities).Get(1);

      Assert.IsTrue(entity == null);
    }

    [TestMethod]
    public void ApplyDefaults_GetByQuery()
    {
      var testEntities = new List<TestEntity>
        {
          new TestEntity(1, TestEntityQueryBuilder.NameToExclude, Stage.Alpha),
          new TestEntity(2, "not_exclude", Stage.Beta)
        };

      TestEntity[] entities = new TestEntitySet(testEntities).Get(new TestEntitySetQuery())
                                                             .ToArray();

      Assert.AreEqual(1, entities.Length);
    }

    [TestMethod]
    [ExpectedException(typeof(EntityNotFoundException<TestEntity>))]
    public void ApplyDefaults_Update()
    {
      var testEntities = new List<TestEntity>
        {
          new TestEntity(1, TestEntityQueryBuilder.NameToExclude, Stage.Alpha),
          new TestEntity(2, "not_exclude", Stage.Beta)
        };

      var changes = new TestEntityChanges
        {
          Id = 1,
          Stage = Stage.Gamma
        };

      TestEntity updatedEntity = new TestEntitySet(testEntities).Update(changes);
    }
  }
}
