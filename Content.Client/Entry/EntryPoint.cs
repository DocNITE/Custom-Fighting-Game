using Content.Client.GameMan;
using Content.Client.Input;
using Content.Client.IoC;
using Content.Client.Novel.Manager;
using Content.Client.States;
using Content.Client.UserInterfaces;
using JetBrains.Annotations;
using Robust.Client;
using Robust.Client.Graphics;
using Robust.Client.Input;
using Robust.Client.State;
using Robust.Client.UserInterface;
using Robust.Shared.ContentPack;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

// DEVNOTE: Games that want to be on the hub can change their namespace prefix in the "manifest.yml" file.
namespace Content.Client.Entry;

[UsedImplicitly]
public sealed class EntryPoint : GameClient
{
    [Dependency] private readonly IUserInterfaceManager _userInterfaceManager = default!;
    [Dependency] private readonly IInputManager _inputManager = default!;
    
    [Dependency] private readonly IGtkUserInterfaceManager _gtkUserInterfaceManager = default!;
    [Dependency] private readonly IVnSceneManager _vnSceneManager = default!;
    [Dependency] private readonly IGameMan _gameMan = default!;

    public override void Init()
    {
        ClientContentIoC.Register();
        
        var factory = IoCManager.Resolve<IComponentFactory>();
        var prototypes = IoCManager.Resolve<IPrototypeManager>();

        factory.DoAutoRegistrations();

        foreach (var ignoreName in IgnoredComponents.List)
        {
            factory.RegisterIgnore(ignoreName);
        }

        foreach (var ignoreName in IgnoredPrototypes.List)
        {
            prototypes.RegisterIgnore(ignoreName);
        }

        IoCManager.BuildGraph();
        IoCManager.InjectDependencies(this);
        
        // Initialize bindigs
        ContentContexts.SetupContexts(_inputManager.Contexts);
        
        // Initialize IoC
        _gtkUserInterfaceManager.Initialize();
        _vnSceneManager.Initialize();
        _gameMan.Initialize();

        factory.GenerateNetIds();
    }

    public override void PostInit()
    {
        base.PostInit();
            
        // Disabled engine light system
        IoCManager.Resolve<ILightManager>().Enabled = false;
        // Disabled engine viewport
        _userInterfaceManager.MainViewport.Visible = false;

        var stateManager = IoCManager.Resolve<IStateManager>();
         stateManager.RequestStateChange<GameplayState>();
         
        var client = IoCManager.Resolve<IBaseClient>();
        client.StartSinglePlayer();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        // Game systems
        _gameMan.Dispose(disposing);
    }

    public override void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
    {
        base.Update(level, frameEventArgs);
        // Game systems
        _gameMan.Update(level, frameEventArgs);
    }
}