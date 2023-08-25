using Content.Client.GameMan.States;
using Robust.Client.Input;
using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Client.GameMan;

public class GameMan : IGameMan
{
    [Dependency] private readonly IInputManager _inputManager = default!;
    [Dependency] private readonly IEntityManager _entityManager = default!;
    
    public IGameState? TargetState { get; private set; }
    
    public void Initialize()
    {
        IoCManager.InjectDependencies(this);
        // NOTE: For default we initialize MenuState
        SetState(new MenuState());
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
            _inputManager.FirstChanceOnKeyEvent -= TargetState.OnInput;
        }
        
        TargetState = state;
        TargetState.Initialize();
        _inputManager.FirstChanceOnKeyEvent += TargetState.OnInput;
        
        // TODO: Send StateChangeEvent for systems
    }
}

