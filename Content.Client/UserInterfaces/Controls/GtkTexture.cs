using System.Numerics;
using Content.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

public sealed class GtkTexture : GtkWidget
{
    public string? TexturePath { get; set; }

    public Color Color { get; set; } = Color.White;
    
    public override void OnDraw(GtkDrawingHandle handle)
    {
        base.OnDraw(handle);

        if (TexturePath is null)
            return;
        
        var tex = new GraphicsTexture(TexturePath);
        DrawTexture(handle,tex,Color);
    }
}