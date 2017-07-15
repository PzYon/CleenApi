using CleenApi.Library.Exceptions;
using CleenApi.Library.Tests.TestImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleenApi.Library.Tests.BaseEntitySetTests
{
  [TestClass]
  public class BaseEntitySetTests_Create
  {
    [TestMethod]
    [ExpectedException(typeof(EntityProcessingException<TestEntity>))]
    public void WithId_Throws()
    {
      new TestEntitySet(TestEntitiesRepo.DefaultEntities).Create(new TestEntityChanges {Id = 1});
    }

    [TestMethod]
    public void EntityChanges_ValuesAreApplied()
    {
      var changes = new TestEntityChanges
        {
          Name = "New name",
          Stage = Stage.Gamma
        };

      TestEntity createdEntity = new TestEntitySet(TestEntitiesRepo.DefaultEntities).Create(changes);

      Assert.AreEqual(changes.Name, createdEntity.Name);
      Assert.AreEqual(changes.Stage, createdEntity.Stage);
    }
  }
}