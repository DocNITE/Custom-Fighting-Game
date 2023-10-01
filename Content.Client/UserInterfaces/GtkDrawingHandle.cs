using System.Numerics;
using Content.Client.Graphics;
using Content.Client.Graphics.Viewport;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces;

public sealed partial class GtkDrawingHandle : IGtkDrawingHandle
{
    private readonly DrawingHandleScreen _handle;
    private readonly IMesaDrawing _drawing;

    public int CurrentRenderScale => _drawing.CurrentRenderScale;

    public GtkDrawingHandle(DrawingHandleScreen handle, IMesaDrawing drawing)
    {
        _handle = handle;
        _drawing = drawing;
    }

    public void DrawGlobalTexture(GraphicsTexture texture, Color? modulate = null) => 
        _drawing.DrawTexture(_handle, texture, modulate);

    public void DrawRectangle(UIBox2 rect, Color modulate, bool filled = false) =>
        _drawing.DrawRectangle(_handle, rect, modulate, filled);
    
    public void DrawRectangle(float fromX, float fromY, float toX, float toY, Color modulate, bool filled = false) =>
        _drawing.DrawRectangle(_handle, new UIBox2(new Vector2(fromX, fromY), new Vector2(toX, toY)), modulate, filled);

    public void DrawLine(Vector2 from, Vector2 to, Color color) => 
        _drawing.DrawLine(_handle, from, to, color);
    
    public void DrawLine(float fromX, float fromY, float toX, float toY, Color color) => 
        _drawing.DrawLine(_handle, new Vector2(fromX, fromY), new Vector2(toX, toY), color);

    public void UseShader(ShaderInstance? shader) =>
        _handle.UseShader(shader);

}