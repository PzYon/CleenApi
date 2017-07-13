using CleenApi.Library.Exceptions;
using CleenApi.Library.Tests.TestImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CleenApi.Library.Tests.BaseEntitySetTests
{
  // todo: global:
  // - test Defaults are considered

  [TestClass]
  public class BaseEntitySetTests_Update
  {
    // todo:
    // - update works
    // - is valid works

    [TestMethod]
    [ExpectedException(typeof(EntityProcessingException<TestEntity>))]
    public void EntityChanges_IdMismatch_Throws()
    {
      new TestEntitySet().Update(new TestEntityChanges {Id = 1}, 2);
    }

    [TestMethod]
    public void EntityChanges_ValuesAreApplied()
    {
      var changes = new TestEntityChanges
        {
          Name = "New name",
          Stage = Stage.Gamma
        };

      TestEntity changedEntity = new TestEntitySet().Update(changes, 1);

      Assert.AreEqual(changes.Name, changedEntity.Name);
      Assert.AreEqual(changes.Stage, changedEntity.Stage);
    }
  }
}