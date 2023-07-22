using Content.Client.States;
using Robust.Client.Graphics;
using Robust.Client.UserInterface.Controllers;

namespace Content.Client.Graphics.Viewport;

public sealed class ViewportUiController : UIController
{

    public static readonly Vector2i ViewportSize = (EyeManager.PixelsPerMeter * 21, EyeManager.PixelsPerMeter * 15);
    public const int ViewportHeight = 15;
    private MainViewport? Viewport => UIManager.ActiveScreen?.GetWidget<MainViewport>();

    public override void Initialize()
    {
        var gameplayStateLoad = UIManager.GetUIController<GameplayStateLoadController>();
        gameplayStateLoad.OnScreenLoad += OnScreenLoad;
    }

    private void OnScreenLoad()
    {
        ReloadViewport();
    }
    
    private void UpdateViewportRatio()
    {
        if (Viewport == null)
        {
            return;
        }

        var min = 15;
        var max = 21;
        var width = 21;

        if (width < min || width > max)
        {
            width = 21; 
        }

        Viewport.Viewport.ViewportSize = (EyeManager.PixelsPerMeter * width, EyeManager.PixelsPerMeter * ViewportHeight);
        Viewport.Viewport.PhysicalSize = (800, 600);
    }

    public void ReloadViewport()
    {
        if (Viewport == null)
        {
            return;
        }

        UpdateViewportRatio();
        Viewport.Viewport.HorizontalExpand = true;
        Viewport.Viewport.VerticalExpand = true;
    }
}
