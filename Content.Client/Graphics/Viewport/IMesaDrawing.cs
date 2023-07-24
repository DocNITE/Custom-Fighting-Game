using System.Numerics;
using Robust.Client.Graphics;

namespace Content.Client.Graphics.Viewport;

public interface IMesaDrawing
{
    int CurrentRenderScale { get; }
    
    Vector2i PhysicalSize { get; set; }  
    
    void DrawTexture(DrawingHandleScreen handle, GraphicsTexture texture, Color? modulate);
    
    void DrawTexture(DrawingHandleScreen handle, GraphicsTexture texture);
    
    void DrawRectangle(DrawingHandleScreen handle, UIBox2 rect, Color modulate, bool filled = false);

    void DrawLine(DrawingHandleScreen handle, Vector2 from, Vector2 to, Color color);

    UIBox2i GetDrawBox();
}
