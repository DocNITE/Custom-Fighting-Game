using Content.Client.Graphics;
using Content.Client.Graphics.Viewport;
using Robust.Client.Graphics;
using TerraFX.Interop.DirectX;

namespace Content.Client.UserInterfaces;

public sealed partial class GtkDrawingHandle : IGtkDrawingHandle
{
    private readonly DrawingHandleScreen _handle;
    private readonly IViewportDrawing _drawing;

    public int CurrentRenderScale => _drawing.CurrentRenderScale;

    public GtkDrawingHandle(DrawingHandleScreen handle, IViewportDrawing drawing)
    {
        _handle = handle;
        _drawing = drawing;
    }

    public void DrawGlobalTexture(GraphicsTexture texture, Color? modulate = null) => _drawing.DrawTexture(_handle, texture, modulate);

    public void UseShader(ShaderInstance? shader) => _handle.UseShader(shader);

}