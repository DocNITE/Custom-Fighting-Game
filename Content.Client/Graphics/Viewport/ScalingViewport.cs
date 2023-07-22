using System.Numerics;
using Content.Client.UserInterfaces;
using Robust.Client.Graphics;
using Robust.Client.Input;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.Utility;
using Robust.Shared.Map;
using Robust.Shared.Utility;
using SixLabors.ImageSharp.PixelFormats;

namespace Content.Client.Graphics.Viewport;

public sealed class ScalingViewport : Control, IViewportControl, IViewportDrawing
{
    [Dependency] private readonly IClyde _clyde = default!;
    [Dependency] private readonly IInputManager _inputManager = default!;
    [Dependency] private readonly IGtkUserInterfaceManager _gtkUserInterfaceManager = default!;

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
        
        //TODO: Make tile drawing (so we need other objects)
        //TODO2: Make entities drawing with SpriteComponent from content code
        //TODO: We should use Color modulate for drawing. If we wanna add some dark tiles idk (just use alpha channel color)
        
        /* Just example
         var texture = new GraphicsTexture("/Textures/Mobs/Cats/wizard.rsi", "dummy")
        {
            Direction = Direction.East
        };
        DrawTexture(handle, texture, drawBox);
        */

        // TODO: Make ui drawing
        var uiManagerRoot = _gtkUserInterfaceManager.RootScreen;
        for (var i = 0; i < uiManagerRoot.ChildCount; i++)
        {
            var widget = uiManagerRoot.Children[i];
            widget.Draw(handle, this);
        }
        
        // draw non used area
        handle.DrawRect(new UIBox2(new Vector2(0,0), new Vector2(Size.X, drawBox.Top)), Color.Black, true);
        handle.DrawRect(new UIBox2(new Vector2(drawBox.Right,0), new Vector2(Size.X, Size.Y)), Color.Black, true);
        handle.DrawRect(new UIBox2(new Vector2(0,drawBox.Bottom), new Vector2(Size.X, Size.Y)), Color.Black, true);
        handle.DrawRect(new UIBox2(new Vector2(0,0), new Vector2(drawBox.Left, Size.Y)), Color.Black, true);
    }

    public UIBox2i GetDrawBox()
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

    public void DrawTexture(DrawingHandleScreen handle, GraphicsTexture texture)
    {
        //var texture = new SpriteSpecifier.Texture(new ResPath("/Textures/Arts/default.png")).DirFrame0().Default;
        // WARNING: Monkey code! It can be make most slowly game work
        var drawBox = GetDrawBox();
        
        var textureScale = texture.Scale;
        var textureSize = texture.Size;
        var textureRect = texture.Rect;
        var texturePosition = texture.Position;
        var textureLocalSize = new Vector2(
            drawBox.Width/( _physicalSize.X / ((textureSize.X*_curRenderScale)*textureScale.X) ), 
            drawBox.Height/( _physicalSize.Y / ((textureSize.Y*_curRenderScale)*textureScale.Y) ));
        var textureLocalPosition = new Vector2((float)drawBox.TopLeft.X, (float)drawBox.TopLeft.Y) + 
                                   texturePosition * new Vector2(
                                       (float)drawBox.Size.X / (float)_physicalSize.X, 
                                       (float)drawBox.Size.X / (float)_physicalSize.X);

        // draw texture
        handle.DrawTextureRectRegion(texture.Texture,
            new UIBox2(
                textureLocalPosition, 
                textureLocalPosition + textureLocalSize),
            textureRect
        );
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