using System.Collections.Generic;

namespace CleenApi.Library.Tests.TestImplementations
{
  public static class TestEntitiesRepo
  {
    public static readonly List<TestEntity> Entities = new List<TestEntity>
      {
        new TestEntity(1, "Roger Federer", Stage.Alpha),
        new TestEntity(2, "Novak Djokovic", Stage.Beta),
        new TestEntity(3, "Rafael Nadal", Stage.Gamma),
        new TestEntity(4, "Stanislas Wawrinka", Stage.Alpha)
      };
  }
}