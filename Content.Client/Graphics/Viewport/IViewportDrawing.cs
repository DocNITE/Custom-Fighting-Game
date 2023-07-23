using System.Numerics;
using Robust.Client.Graphics;

namespace Content.Client.Graphics.Viewport;

public interface IViewportDrawing
{
    int CurrentRenderScale { get; }
    Vector2i PhysicalSize { get; set; }  
    void DrawTexture(DrawingHandleScreen handle, GraphicsTexture texture, Color? modulate);
    void DrawTexture(DrawingHandleScreen handle, GraphicsTexture texture);
    UIBox2i GetDrawBox();
}
