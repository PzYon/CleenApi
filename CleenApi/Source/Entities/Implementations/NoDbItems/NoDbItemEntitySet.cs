using System.Linq;
using CleenApi.Entities.Queries;
using CleenApi.Entities.Queries.Builder;

namespace CleenApi.Entities.Implementations.NoDbItems
{
  public class NoDbItemEntitySet : IEntitySet<NoDbItem, NoDbItemChanges>
  {
    public NoDbItem Get(int id, string[] includes = null)
    {
      return NoDbItemsRepo.Items.SingleOrDefault(i => i.Id == id);
    }

    public IQueryable<NoDbItem> Get(EntitySetQuery query)
    {
      IQueryable<NoDbItem> noDbItems = NoDbItemsRepo.Items.AsQueryable();

      return query == null
               ? noDbItems
               : new EntityQueryBuilder<NoDbItem>().Build(noDbItems, query);
    }

    public NoDbItem Update(NoDbItemChanges entityChanges)
    {
      NoDbItem item;
      if (!entityChanges.Id.HasValue)
      {
        item = new NoDbItem
          {
            Id = NoDbItemsRepo.Items.OrderByDescending(i => i.Id).Select(i => i.Id).First()
          };
        NoDbItemsRepo.Items.Add(item);
      }
      else
      {
        item = Get(entityChanges.Id.Value);
      }

      return entityChanges.ApplyValues(null, item);
    }

    public void Delete(int id)
    {
      NoDbItem item = Get(id);
      NoDbItemsRepo.Items.Remove(item);
    }

    public void Dispose()
    {
    }
  }
}