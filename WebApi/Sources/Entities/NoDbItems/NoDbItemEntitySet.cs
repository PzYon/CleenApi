using System.Linq;
using CleenApi.Library.EntitySets;

namespace CleenApi.WebApi.Entities.NoDbItems
{
  public class NoDbItemEntitySet : BaseEntitySet<NoDbItem, NoDbItemChanges, NoDbItemQueryBuilder>
  {
    protected override IQueryable<NoDbItem> Entities => NoDbItemsRepo.Items.AsQueryable();

    protected override NoDbItem Create()
    {
      return new NoDbItem
        {
          Id = NoDbItemsRepo.Items.OrderByDescending(i => i.Id)
                            .Select(i => i.Id)
                            .First()
        };
    }

    protected override NoDbItem Store(NoDbItem entity)
    {
      NoDbItemsRepo.Items.Add(entity);
      return entity;
    }

    protected override void Delete(NoDbItem entity)
    {
      NoDbItemsRepo.Items.Remove(entity);
    }

    public override void Dispose()
    {
    }
  }
}