using Content.Client.UserInterfaces.Screens;
using Robust.Client.State;
using Robust.Client.UserInterface;

namespace Content.Client.States;

public sealed class GameplayState : State
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] protected readonly IUserInterfaceManager UserInterfaceManager = default!;

    private readonly StateLoadController _loadController;
    public GameplayState()
    {
        IoCManager.InjectDependencies(this);
        
        _loadController = UserInterfaceManager.GetUIController<StateLoadController>();
    }

    protected override void Startup()
    {
        UserInterfaceManager.LoadScreen<GameplayScreen>();
        _loadController.LoadScreen();
    }

    protected override void Shutdown()
    {
        UserInterfaceManager.UnloadScreen();
        _loadController.UnloadScreen();
    }
}