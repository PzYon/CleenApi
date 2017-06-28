using System.Collections.Generic;
using System.Linq;

namespace CleenApi.Entities
{
  public interface IEntityQuery<TEntity> where TEntity : class, IEntity
  {
    IQueryable<TEntity> Build(IQueryable<TEntity> set, KeyValuePair<string, string>[] conditions);
  }
}