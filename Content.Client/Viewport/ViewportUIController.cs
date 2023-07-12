using Content.Client.States;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Client.UserInterface.Controllers;
using Robust.Shared.Configuration;
using Robust.Shared.Timing;
using Content.Client.UserInterfaces.Screens;

namespace Content.Client.Viewport;

public sealed class ViewportUIController : UIController
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

        var min = 640;//_configurationManager.GetCVar(CCVars.ViewportMinimumWidth);
        var max = 1280;//_configurationManager.GetCVar(CCVars.ViewportMaximumWidth);
        var width = 800;//_configurationManager.GetCVar(CCVars.ViewportWidth);

        if (width < min || width > max)
        {
            width = 800; //CCVars.ViewportWidth.DefaultValue;
        }

        Viewport.Viewport.ViewportSize = (EyeManager.PixelsPerMeter * width, EyeManager.PixelsPerMeter * ViewportHeight);
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
