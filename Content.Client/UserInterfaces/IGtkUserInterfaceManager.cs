using Content.Client.Graphics.Viewport;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces;

public interface IGtkUserInterfaceManager
{
    GtkWidget RootScreen { get; }

    void Initialize();

    void DrawWidgets(DrawingHandleScreen handle, IViewportDrawing methods);
}
