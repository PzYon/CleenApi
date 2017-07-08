﻿using CleenApi.Entities;
using CleenApi.Queries.LinqUtilities;

namespace CleenApi.Queries.QueryBuilders
{
  public class DbEntityQueryBuilder<TEntity> : BaseEntityQueryBuilder<TEntity, DbLinqUtility>
    where TEntity : IEntity
  {
  }
}