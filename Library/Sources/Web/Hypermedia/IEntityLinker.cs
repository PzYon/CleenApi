using System;
using System.Collections.Generic;

namespace CleenApi.Library.Web.Hypermedia
{
  public interface IEntityLinker
  {
    List<Type> GetSupportedEntityTypes();

    List<Link> GetLinks(object entity);
  }
}