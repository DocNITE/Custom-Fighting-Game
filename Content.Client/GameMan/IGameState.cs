using Content.Client.UserInterfaces.Controls;
using Robust.Client.Input;
using Robust.Shared.Input;

namespace Content.Client.GameMan;

public interface IGameState
{
    GtkWidget Viewport { get; }

    void Initialize();

    bool OnInput(BoundKeyEventArgs arg);
}
