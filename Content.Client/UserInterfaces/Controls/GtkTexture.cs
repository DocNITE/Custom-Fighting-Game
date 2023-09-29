using System.Numerics;
using Content.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

public sealed class GtkTexture : GtkWidget
{
    public string? TexturePath { get; set; }

    public Color Color { get; set; } = Color.White;

    public UIBox2? Rect { get; set; }

    public override bool OnDraw(GtkDrawingHandle handle)
    {
        if (TexturePath is null)
            return false;

        var tex = new GraphicsTexture(TexturePath);
        tex.Size = Size;
        if (Rect is not null)
            tex.Rect = (UIBox2)Rect;
        DrawTexture(handle, tex, Color);

        return base.OnDraw(handle);
    }
}
