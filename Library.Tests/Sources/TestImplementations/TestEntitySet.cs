using System.Linq;
using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntitySet : BaseEntitySet<TestEntity, TestEntityChanges, TestEntityQueryBuilder>
  {
    protected override IQueryable<TestEntity> Entities => TestEntitiesRepo.Entities.AsQueryable();

    protected override TestEntity Create()
    {
      return new TestEntity();
    }

    protected override TestEntity Store(TestEntity entity)
    {
      if (entity.Id == 0)
      {
        int lastId = Entities.OrderByDescending(e => e.Id).First().Id;
        entity.Id = lastId + 1;
      }
      else
      {
        TestEntitiesRepo.Entities.Remove(TestEntitiesRepo.Entities.First(e => e.Id == entity.Id));
      }

      TestEntitiesRepo.Entities.Add(entity);
      return entity;
    }

    protected override void Delete(TestEntity entity)
    {
      TestEntitiesRepo.Entities.Remove(entity);
    }

    public override void Dispose()
    {
    }
  }
}