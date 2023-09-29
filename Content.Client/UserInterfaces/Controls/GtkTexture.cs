using System.Numerics;
using Content.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

public sealed class GtkTexture : GtkWidget
{
    public string? TexturePath { get; set; }

    public Color Color { get; set; } = Color.White;
    
    public override bool OnDraw(GtkDrawingHandle handle)
    {
        if (!base.OnDraw(handle))
            return false;

        if (TexturePath is null)
            return false;
        
        var tex = new GraphicsTexture(TexturePath);
        DrawTexture(handle,tex,Color);

        return true;
    }
}