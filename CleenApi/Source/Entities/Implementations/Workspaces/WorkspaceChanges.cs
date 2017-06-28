namespace CleenApi.Entities.Implementations.Workspaces
{
  public class WorkspaceChanges : IEntityChanges<Workspace>
  {
    public int? Id { get; set; }

    public string Title { get; set; }

    public Workspace ApplyValues(Workspace workspace)
    {
      if (!string.IsNullOrEmpty(Title))
      {
        workspace.Title = Title;
      }

      return workspace;
    }
  }
}