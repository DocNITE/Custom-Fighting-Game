using System.Numerics;
using Content.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

public sealed class GtkTexture : GtkWidget
{
    public string? TexturePath { get; set; }

    public override void Draw(GtkDrawingHandle handle)
    {
        base.Draw(handle);

        if (TexturePath is null)
            return;
        
        var tex = new GraphicsTexture(TexturePath);
        tex.Scale = new Vector2(2, 2);
        DrawTexture(handle,tex);
    }
}