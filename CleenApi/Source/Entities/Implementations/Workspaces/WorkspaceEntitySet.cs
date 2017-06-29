namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceEntitySet : BaseEntitySet<Workspace, WorkspaceChanges, WorkspaceQuery>
  {
    public int GetLikes(int id)
    {
      return GetById(id).Likes;
    }

    public int UpdateLikes(int id)
    {
      Workspace w = GetById(id);
      w.Likes++;

      Db.AddOrUpdate(w);

      return w.Likes;
    }
  }
}