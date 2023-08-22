using Content.Client.UserInterfaces.Controls;
using Robust.Client.Input;

namespace Content.Client.GameMan;

public interface IGameState
{
    GtkWidget Viewport { get; }

    void Initialize() {}

    void OnInput(KeyEventArgs keyEvent, KeyEventType type) {}
}
