using System.Collections.Generic;
using System.Linq;
using CleenApi.Library.EntitySets;

namespace CleenApi.Library.Tests.TestImplementations
{
  public class TestEntitySet : BaseEntitySet<TestEntity, TestEntityChanges, TestEntityQueryBuilder>
  {
    protected override IQueryable<TestEntity> Entities => RepoEntities.AsQueryable();

    public List<TestEntity> RepoEntities { get; }

    protected override TestEntity Create()
    {
      return new TestEntity();
    }

    public TestEntitySet(List<TestEntity> repoEntities)
    {
      RepoEntities = repoEntities;
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
        RepoEntities.Remove(RepoEntities.First(e => e.Id == entity.Id));
      }

      RepoEntities.Add(entity);
      return entity;
    }

    protected override void Delete(TestEntity entity)
    {
      RepoEntities.Remove(entity);
    }

    public override void Dispose()
    {
    }
  }
}