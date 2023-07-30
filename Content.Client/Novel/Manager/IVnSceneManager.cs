namespace Content.Client.Novel.Manager;

public interface IVnSceneManager
{
    VnSceneData? CurrentScene { get; set; }
    void Initialize();
    void LoadScene(string prototype);
    void UnloadScene();
}
