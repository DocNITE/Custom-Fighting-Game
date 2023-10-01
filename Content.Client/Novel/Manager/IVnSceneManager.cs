using Content.Client.GameMan.States;

namespace Content.Client.Novel.Manager;

public enum DialogStyle
{
    // Background style, like 3rd story
    Default = 0,
    // For character talk (with positioning)
    CharacterLeft = 1,
    CharacterRight = 2,
    CharacterCenter = 3,
    // Author saying
    Author = 4,
}

public interface IVnSceneManager
{
    VnScenePrototype? CurrentScene { get; set; }
    DialogStyle DialogStyle { get; set; }

    void Initialize();
    void LoadScene(string prototype);
    void Continue();
    void CleanupScene();

    GtkNovelScreen? GetStateGui();
}
