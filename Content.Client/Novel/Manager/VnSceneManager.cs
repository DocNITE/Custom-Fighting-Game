using Content.Client.GameMan;
using Robust.Shared.Prototypes;

namespace Content.Client.Novel.Manager;

public sealed class VnSceneManager : IVnSceneManager
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly IGameMan _gameMan = default!;
    
    private ISawmill _logger = default!;
    
    public VnSceneData? CurrentScene { get; set; }
    public int Stage { get; private set; } = 0;
    
    public void Initialize()
    {
        _logger = Logger.GetSawmill("vnSceneManager");
        IoCManager.InjectDependencies(this);
    }

    public void LoadScene(string prototype)
    {
        UnloadScene();

        try
        {
            var scene = _protoManager.Index<VnSceneData>(prototype);
            CurrentScene = scene;
        }
        catch (Exception e)
        {
            _logger.Error("Cannot load scene '"+prototype+"'! Exception: " + e);
            return;
        }
    }

    public void Continue()
    {
        // some do
    }

    public void Clear()
    {
        // We should there clear all text
    }

    public void UnloadScene()
    {
        Clear();
        CurrentScene = null;
    }
}

//TODO Надо придумать, как взаимодействовать с UI из NovelState через акшены и VnSceneManager