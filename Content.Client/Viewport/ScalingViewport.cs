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
    private int _curRenderScale;
    private ScalingViewportStretchMode _stretchMode = ScalingViewportStretchMode.Bilinear;
    private ScalingViewportRenderScaleMode _renderScaleMode = ScalingViewportRenderScaleMode.Fixed;
    private int _fixedRenderScale = 1;

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
        //base.Draw(handle);
        
        //handle.DrawRect(new UIBox2(Vector2.Zero, this.Size), Color.Green, true);
        //handle.SetTransform(new(32, 32), 40);
        //handle.DrawTexture(new SpriteSpecifier.Texture(new ResPath("/Textures/Arts/chel.png")).DirFrame0().Default, new(400, 400));
        
        EnsureViewportCreated();

        var drawBox = GetDrawBox();
        var drawBoxGlobal = drawBox.Translated(GlobalPixelPosition);
        // some govno ex 2
        var tex = Texture.White;
        handle.DrawTextureRect(tex, drawBox);
        // Some govno ex
        //handle.DrawTextureRect(new SpriteSpecifier.Texture(new ResPath("/Textures/Arts/chel.png")).DirFrame0().Default, drawBox);
        // Drawbox example
        //handle.DrawTexture(new SpriteSpecifier.Texture(new ResPath("/Textures/Arts/chel.png")).DirFrame0().Default, new(400, 400));
        // SS14 rendering
        //_viewport.RenderScreenOverlaysBelow(handle, this, drawBoxGlobal);
        //handle.DrawTextureRect(_viewport.RenderTarget.Texture, drawBox);
        //_viewport.RenderScreenOverlaysAbove(handle, this, drawBoxGlobal);
    }

    public void Screenshot(CopyPixelsDelegate<Rgba32> callback)
    {
        _queuedScreenshots.Add(callback);
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
    
    private void RegenerateViewport()
    {

        var vpSizeBase = ViewportSize;
        var ourSize = PixelSize;
        var (ratioX, ratioY) = ourSize / (Vector2) vpSizeBase;
        var ratio = Math.Min(ratioX, ratioY);
        var renderScale = 1;
        switch (_renderScaleMode)
        {
            case ScalingViewportRenderScaleMode.CeilInt:
                renderScale = (int) Math.Ceiling(ratio);
                break;
            case ScalingViewportRenderScaleMode.FloorInt:
                renderScale = (int) Math.Floor(ratio);
                break;
            case ScalingViewportRenderScaleMode.Fixed:
                renderScale = _fixedRenderScale;
                break;
        }

        // Always has to be at least one to avoid passing 0,0 to the viewport constructor
        renderScale = Math.Max(1, renderScale);

        _curRenderScale = renderScale;
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
        
    }

    public MapCoordinates ScreenToMap(Vector2 coords)
    {
        throw new NotImplementedException();
    }

    public Vector2 WorldToScreen(Vector2 map)
    {
        throw new NotImplementedException();
    }

    public Matrix3 GetWorldToScreenMatrix()
    {
        throw new NotImplementedException();
    }

    public Matrix3 GetLocalToScreenMatrix()
    {
        throw new NotImplementedException();
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