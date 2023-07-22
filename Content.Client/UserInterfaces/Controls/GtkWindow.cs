using System.Numerics;
using Content.Client.Graphics;
using Content.Client.Graphics.Viewport;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

[Virtual]
public partial class GtkWindow : GtkWidget
{
    public string Content = "";
    
    public override void Draw(DrawingHandleScreen handle, IViewportDrawing drawingMethods)
    {
        base.Draw(handle, drawingMethods);

        var tex1 = new GraphicsTexture("/Textures/Interface/Window.png");
        tex1.Rect = new UIBox2(Vector2.Zero, new Vector2(16, 16));
        tex1.Size = new Vector2(16, 16);
        tex1.Position = new Vector2(0, 0);
        var tex2 = new GraphicsTexture("/Textures/Interface/Window.png");
        tex2.Rect = new UIBox2(new Vector2(16, 0), new Vector2(32, 16));
        tex2.Size = new Vector2(16, 16);
        tex2.Position = new Vector2(16*2, 0);
        var tex3 = new GraphicsTexture("/Textures/Interface/Window.png");
        tex3.Rect = new UIBox2(new Vector2(16, 0), new Vector2(32, 16));
        tex3.Size = new Vector2(16, 16);
        tex3.Position = new Vector2(32*2, 0);
        
        drawingMethods.DrawTexture(handle, tex1);
        drawingMethods.DrawTexture(handle, tex2);
        drawingMethods.DrawTexture(handle, tex3);
    }
}