using Content.Client.UserInterfaces;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Input;

namespace Content.Client.GameMan;

public class GameState : IGameState
{
    public GtkWidget Viewport { get; set; } = default!;

    public virtual void Initialize()
    {
        IoCManager.Resolve<IGtkUserInterfaceManager>().RootScreen.AddChild(Viewport);
    }
    
    public virtual void OnInput(KeyEventArgs keyEvent, KeyEventType type)
    {
    }
}