using Robust.Client;
using Robust.Client.GameObjects;
using Robust.Client.Player;
using Robust.Client.Input;
using Robust.Shared.Timing;

namespace Content.Client.Input;

/// <summary>
///     This class listens for input events and sends them to the server.
/// </summary>
public sealed class InputHookupManager
{
    [Dependency] private readonly IInputManager _inputManager = default!;
    [Dependency] private readonly IEntitySystemManager _entitySystemManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly IBaseClient _baseClient = default!;

    public void Initialize()
    {
        /*
         * IoCManager.InjectDependencies(this);
        
        _inputManager.KeyBindStateChanged += OnKeyBindStateChanged;

        _inputManager.FirstChanceOnKeyEvent += (@event, type) =>
        {
            Logger.Debug(@event.Key.ToString() + $" ({type.ToString()})");
        };
         */
        // TODO: Make some custom input implementation for our local viewport and ui
        // Yeah, i disabled some code here. It's unnecessary
    }

    private void OnKeyBindStateChanged(ViewportBoundKeyEventArgs args)
    {
        if (_baseClient.RunLevel < ClientRunLevel.InGame)
            return;
            
        if (!_entitySystemManager.TryGetEntitySystem<InputSystem>(out var inputSystem))
            return;
        
        //Logger.Debug(args.KeyEventArgs.Function.ToString() ?? string.Empty);
        //var message = new FullInputCmdMessage(_gameTiming.CurTick, _gameTiming.TickFraction, _inputManager.NetworkBindMap.KeyFunctionID(args.KeyEventArgs.Function), args.KeyEventArgs.State, EntityCoordinates.Invalid, args.KeyEventArgs.PointerLocation, EntityUid.Invalid);
        //if (inputSystem.HandleInputCommand(_playerManager.LocalPlayer!.Session, args.KeyEventArgs.Function, message))
        //    args.KeyEventArgs.Handle();
    }
}