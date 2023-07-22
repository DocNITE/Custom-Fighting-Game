using Content.Client.States;
using Content.Client.UserInterfaces;
using JetBrains.Annotations;
using Robust.Client;
using Robust.Client.Graphics;
using Robust.Client.State;
using Robust.Client.UserInterface;
using Robust.Shared.ContentPack;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

// DEVNOTE: Games that want to be on the hub can change their namespace prefix in the "manifest.yml" file.
namespace Content.Client;

[UsedImplicitly]
public sealed class EntryPoint : GameClient
{
    [Dependency] private readonly IUserInterfaceManager _userInterfaceManager = default!;
    [Dependency] private readonly IGtkUserInterfaceManager _gtkUserInterfaceManager = default!;

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
        
        // Initialize IoC
        _gtkUserInterfaceManager.Initialize();

        factory.GenerateNetIds();
    }

    public override void PostInit()
    {
        base.PostInit();
            
        // Disabled engine light system
        IoCManager.Resolve<ILightManager>().Enabled = false;

        var stateManager = IoCManager.Resolve<IStateManager>();
        
        _userInterfaceManager.MainViewport.Visible = false;
         stateManager.RequestStateChange<GameplayState>();
         
        var client = IoCManager.Resolve<IBaseClient>();
        client.StartSinglePlayer();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        // DEVNOTE: You might want to do a proper shutdown here.
    }

    public override void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
    {
        base.Update(level, frameEventArgs);
        // DEVNOTE: Game update loop goes here. Usually you'll want some independent GameTicker.
    }
}