using Robust.Client.Graphics;
using Robust.Client.Input;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.Utility;
using Robust.Shared.Map;
using Robust.Shared.Utility;
using SixLabors.ImageSharp.PixelFormats;

namespace Content.Client.Viewport;

public sealed class ScalingViewport : Control, IViewportControl
{
    [Dependency] private readonly IClyde _clyde = default!;
    [Dependency] private readonly IInputManager _inputManager = default!;

    private Vector2i _viewportSize;
    private Vector2i _physicalSize;
    private int _curRenderScale;
    private ScalingViewportStretchMode _stretchMode = ScalingViewportStretchMode.Bilinear;
    private ScalingViewportRenderScaleMode _renderScaleMode = ScalingViewportRenderScaleMode.Fixed;
    private int _fixedRenderScale = 1;

    private bool _initialized = false;

    private readonly List<CopyPixelsDelegate<Rgba32>> _queuedScreenshots = new();

    public int CurrentRenderScale => _curRenderScale;
    
    public Vector2i ViewportSize
    {
        get => _viewportSize;
        set
        {
            _viewportSize = value;
            InvalidateViewport();
        }
    }
    
    public Vector2i PhysicalSize
    {
        get => _physicalSize;
        set
        {
            _physicalSize = value;
            InvalidateViewport();
        }
    }

    // Do not need to InvalidateViewport() since it doesn't affect viewport creation.

    [ViewVariables(VVAccess.ReadWrite)] public Vector2i? FixedStretchSize { get; set; }

    [ViewVariables(VVAccess.ReadWrite)]
    public ScalingViewportStretchMode StretchMode
    {
        get => _stretchMode;
        set
        {
            _stretchMode = value;
            InvalidateViewport();
        }
    }

    [ViewVariables(VVAccess.ReadWrite)]
    public ScalingViewportRenderScaleMode RenderScaleMode
    {
        get => _renderScaleMode;
        set
        {
            _renderScaleMode = value;
            InvalidateViewport();
        }
    }

    [ViewVariables(VVAccess.ReadWrite)]
    public int FixedRenderScale
    {
        get => _fixedRenderScale;
        set
        {
            _fixedRenderScale = value;
            InvalidateViewport();
        }
    }
    
    public ScalingViewport()
    {
        IoCManager.InjectDependencies(this);
        RectClipContent = true;
    }

    protected override void Draw(DrawingHandleScreen handle)
    {
        EnsureViewportCreated();

        var drawBox = GetDrawBox();
        
        // Clear screen
        handle.DrawRect(drawBox, Color.Black, true);

        var texture = new SpriteSpecifier.Texture(new ResPath("/Textures/Arts/default.png")).DirFrame0().Default;
        var textureSize = new Vector2(32, 32);
        var textureLocalSize = new Vector2(
            drawBox.Height/( _physicalSize.Y / (textureSize.X*_curRenderScale) ), 
            drawBox.Height/( _physicalSize.Y / (textureSize.Y*_curRenderScale) ));
        
        for (int i = 0; i < 1024; i++)
        {
            handle.DrawTextureRectRegion(texture,
                new UIBox2(new Vector2(0,0), textureLocalSize),
                new UIBox2(new Vector2(0,0), textureSize));
        }
    }

    private UIBox2i GetDrawBox()
    {

        var vpSize = ViewportSize;
        var ourSize = (Vector2) PixelSize;

        if (FixedStretchSize == null)
        {
            var (ratioX, ratioY) = ourSize / vpSize;
            var ratio = Math.Min(ratioX, ratioY);

            var size = vpSize * ratio;
            // Size
            var pos = (ourSize - size) / 2;

            return (UIBox2i) UIBox2.FromDimensions(pos, size);
        }
        else
        {
            // Center only, no scaling.
            var pos = (ourSize - FixedStretchSize.Value) / 2;
            return (UIBox2i) UIBox2.FromDimensions(pos, FixedStretchSize.Value);
        }
    }

    protected override void Resized()
    {
        base.Resized();

        InvalidateViewport();
    }

    private void InvalidateViewport()
    {
        
    }
    
    private void EnsureViewportCreated()
    {
        if (_initialized) return;
        _curRenderScale = 2;
        _initialized = !_initialized;
    }

    public MapCoordinates ScreenToMap(Vector2 coords)
    {
        return new MapCoordinates();
    }

    public Vector2 WorldToScreen(Vector2 map)
    {
        return new Vector2();
    }

    public Matrix3 GetWorldToScreenMatrix()
    {
        return new Matrix3();
    }

    public Matrix3 GetLocalToScreenMatrix()
    {
        return new Matrix3();
    }
}

public enum ScalingViewportStretchMode
{
    /// <summary>
    ///     Bilinear sampling is used.
    /// </summary>
    Bilinear = 0,

    /// <summary>
    ///     Nearest neighbor sampling is used.
    /// </summary>
    Nearest,
}

/// <summary>
///     Defines how the base render scale of the viewport is selected.
/// </summary>
public enum ScalingViewportRenderScaleMode
{
    /// <summary>
    ///     <see cref="ScalingViewport.FixedRenderScale"/> is used.
    /// </summary>
    Fixed = 0,

    /// <summary>
    ///     Floor to the closest integer scale possible.
    /// </summary>
    FloorInt,

    /// <summary>
    ///     Ceiling to the closest integer scale possible.
    /// </summary>
    CeilInt
}