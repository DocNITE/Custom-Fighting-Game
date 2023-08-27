using System.Numerics;
using Content.Client.Graphics.Viewport;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Graphics;
using Robust.Client.Input;
using Robust.Client.UserInterface.CustomControls;
using Robust.Shared.Configuration;
using Robust.Shared.Input;
using Robust.Shared.Input.Binding;
using Robust.Shared.Timing;

namespace Content.Client.UserInterfaces;

internal sealed partial class GtkUserInterfaceManager : IGtkUserInterfaceManager
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IInputManager _inputManager = default!;
    
    public GtkWidget RootScreen { get; private set; } = default!;

    public int CurrentRenderScale { get; set; } = 1;

    public void Initialize()
    {
        IoCManager.InjectDependencies(this);
        
        RootScreen = new GtkWidget();
        RootScreen.Size = new Vector2(_cfg.GetCVar<int>("viewport.physical_width"), _cfg.GetCVar<int>("viewport.physical_height"));
        
        _inputManager.UIKeyBindStateChanged += OnUIKeyBindStateChanged;
    }

    public void Draw(DrawingHandleScreen handle, IMesaDrawing methods)
    {
        var gtkHandle = new GtkDrawingHandle(handle, methods);
        CurrentRenderScale = methods.CurrentRenderScale;
        // Draw main root content
        RootScreen.DrawChilds(gtkHandle);
    }

    public void FrameUpdate(FrameEventArgs args)
    {
        var totalUpdated = RootScreen.DoFrameUpdateRecursive(args);
    }
}