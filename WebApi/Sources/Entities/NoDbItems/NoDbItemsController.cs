using CleenApi.Library.Exceptions;
using CleenApi.Library.Web.Controllers;

namespace CleenApi.WebApi.Entities.NoDbItems
{
  public class NoDbItemsController : BaseEntitySetController<NoDbItem, NoDbItemEntitySet, NoDbItemChanges>
  {
    public override void Delete(int id)
    {
      throw new EntityProcessingException<NoDbItem>($"Deleting {nameof(NoDbItem)}s is not supported.");
    }
  }
}