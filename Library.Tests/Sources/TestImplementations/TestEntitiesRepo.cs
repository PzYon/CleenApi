using System.Collections.Generic;

namespace CleenApi.Library.Tests.TestImplementations
{
  public static class TestEntitiesRepo
  {
    public static List<TestEntity> DefaultEntities => new List<TestEntity>
      {
        new TestEntity(1, "Roger Federer", Stage.Alpha),
        new TestEntity(2, "Novak Djokovic", Stage.Beta),
        new TestEntity(3, "Rafael Nadal", Stage.Gamma),
        new TestEntity(4, "Stanislas Wawrinka", Stage.Alpha, "from the same country as roger."),
        new TestEntity(5, "Marin Cilic", Stage.Beta, "Is from Croatia")
      };
  }
}