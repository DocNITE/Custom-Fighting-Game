namespace Content.Client.Novel.Manager;

public interface IVnSceneManager
{
    VnSceneData? CurrentScene { get; set; }
    int Stage { get; }
    void Initialize();
    void LoadScene(string prototype);
    void Continue();
    void Clear();
    void UnloadScene();
}
