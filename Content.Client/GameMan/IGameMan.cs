using Robust.Shared.ContentPack;
using Robust.Shared.Timing;

namespace Content.Client.GameMan;

public interface IGameMan
{
    IGameState TargetState { get; }
    void Initialize();
    void Update(ModUpdateLevel level, FrameEventArgs frameEventArgs);
    void Dispose(bool disposing);
    // need:
    // make tile storage. We dont use entity in storage
    // render target. It's might be GtkWidget like Viewport
    // make some IGameState, and make 3 main states: 1. menu, 2. world, 3. fighting
    // so - we need make some 'StateTarget'
    // render target. It's might be GtkWidget like Viewport
    // so, we move render target to IGameState class.
    // and we need make key input overriding from InputHookupManager
}

