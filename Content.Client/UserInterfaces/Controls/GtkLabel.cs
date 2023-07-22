using Content.Client.Graphics.Viewport;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

[Virtual]
public partial class GtkLabel : GtkWidget
{
    public string Content = "";
    
    public override void Draw(DrawingHandleScreen handle, IViewportDrawing drawingMethods)
    {
        base.Draw(handle, drawingMethods);
        
        //TODO: Need make font drawing from picture with chad ascii code.
        //drawingMethods.DrawTexture(handle);
    }
}