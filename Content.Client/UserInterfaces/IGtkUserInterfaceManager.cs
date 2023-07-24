using Content.Client.Graphics.Viewport;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Graphics;
using Robust.Shared.Timing;

namespace Content.Client.UserInterfaces;

public interface IGtkUserInterfaceManager
{
    int CurrentRenderScale { get; set; }
    
    GtkWidget RootScreen { get; }

    void Initialize();

    void Draw(DrawingHandleScreen handle, IMesaDrawing methods);

    void FrameUpdate(FrameEventArgs args);
}
