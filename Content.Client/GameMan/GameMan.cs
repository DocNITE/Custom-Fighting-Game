using Content.Client.GameMan.States;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Input;
using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Client.GameMan;

public class GameMan : IGameMan
{
    [Dependency] private readonly IInputManager _inputManager = default!;

    public IGameState? TargetState { get; private set; }

    public void Initialize()
    {
        IoCManager.InjectDependencies(this);
        // NOTE: For default we initialize MenuState
        //SetState(new MenuState());
        SetState(new NovelState());
    }

    public void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs)
    {
    }

    public void Dispose(bool disposing)
    {
    }

    public void SetState(GameState state)
    {
        if (TargetState != null)
        {
            _inputManager.UIKeyBindStateChanged -= TargetState.OnInput;
        }

        TargetState?.Dispose();
        TargetState = state;
        TargetState.Initialize();
        _inputManager.UIKeyBindStateChanged += TargetState.OnInput;

        // TODO: Send StateChangeEvent for systems
    }

    public GtkWidget GetStateGui()
    {
        return TargetState != null ? TargetState.Viewport : new GtkWidget();
    }
}

