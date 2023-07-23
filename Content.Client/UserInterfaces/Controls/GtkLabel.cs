using System.Numerics;
using System.Text;
using Content.Client.Graphics.Fonts;
using Content.Client.Graphics.Viewport;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

[Virtual]
public partial class GtkLabel : GtkWidget
{
    public string Content = "Hey Cinka... Do you wanna some novel? :3 |||||";
    public string FontId = "kitchen-sink";
    
    public override void Draw(DrawingHandleScreen handle, IViewportDrawing drawingMethods)
    {
        base.Draw(handle, drawingMethods);

        // TODO Make text wrap, with widget size
        // TODO Make local position and global position for widget
        // we might be make some GtkWidget generic
        // TODO And yeah. We can check texture pixel, so need make normal sizer and rect char
        
        var bytes = Encoding.ASCII.GetBytes(Content);
        var xPos = 0.0f;
        foreach (var item in bytes)
        {
            var tex = new FontTexture(FontId, item, FontStyle.Normal);
            tex.Position = new Vector2(xPos, tex.Position.Y);

            xPos += (tex.Size.X + 1) * drawingMethods.CurrentRenderScale;
            
            drawingMethods.DrawTexture(handle, tex);
        }
    }
}