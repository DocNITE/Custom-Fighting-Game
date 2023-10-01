using Content.Client.UserInterfaces.Screens;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.State;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Screens;
using Robust.Shared.Player;

namespace Content.Client.States;

public sealed class MainScreenState : State
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] protected readonly IUserInterfaceManager UserInterfaceManager = default!;
    
    public MainScreenState()
    {
        IoCManager.InjectDependencies(this);
        
    }

    protected override void Startup()
    {
        UserInterfaceManager.LoadScreen<MainScreen>();
    }

    protected override void Shutdown()
    {
        UserInterfaceManager.UnloadScreen();
    }
}