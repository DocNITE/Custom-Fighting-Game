namespace Content.Client.Novel.Manager;

// TODO: Need create GtkNovel element, before it need make input system
public sealed class VnSceneManager : IVnSceneManager
{
    public VnSceneData? CurrentScene { get; set; }
    
    public void Initialize()
    {
        // Some imlp
    }

    public void LoadScene(string prototype)
    {
        // some do
    }

    public void UnloadScene()
    {
        // some do
    }
}