using Content.Client.UserInterfaces.Screens;
using Robust.Client.State;
using Robust.Client.UserInterface;

namespace Content.Client.States;

public sealed class GameplayState : State
{
    [Dependency] private readonly IEntityManager _entManager = default!;
    [Dependency] protected readonly IUserInterfaceManager UserInterfaceManager = default!;

    public GameplayState()
    {
        IoCManager.InjectDependencies(this);
    }

    protected override void Startup()
    {
        UserInterfaceManager.LoadScreen<GameplayScreen>();
    }

    protected override void Shutdown()
    {
    }
}