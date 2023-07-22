using Robust.Client.Graphics;

namespace Content.Client.Graphics.Viewport;

public interface IViewportDrawing
{
    void DrawTexture(DrawingHandleScreen handle, GraphicsTexture texture);
    UIBox2i GetDrawBox();
}
