using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Configuration;

namespace Content.Client.Graphics.Viewport;

[Virtual]
public partial class MainViewport : UIWidget
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    
    public ScalingViewport Viewport { get; }
    
    public MainViewport()
    {
        IoCManager.InjectDependencies(this);
        
        Viewport = new ScalingViewport
        {
            AlwaysRender = true,
            RenderScaleMode = ScalingViewportRenderScaleMode.CeilInt,
            MouseFilter = MouseFilterMode.Stop
        };

        AddChild(Viewport);
    }
}