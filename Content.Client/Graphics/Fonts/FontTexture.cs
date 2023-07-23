using System.Numerics;
using System.Text;
using Robust.Shared.Prototypes;

namespace Content.Client.Graphics.Fonts;

public sealed class FontTexture : GraphicsTexture
{
    private readonly string _fontId;
    
    public FontTexture(string fontId, char c) : base(IoCManager.Resolve<IPrototypeManager>()
        .Index<FontPrototype>(fontId).Atlas)
    {
        _fontId = fontId;
        CreateTexture(Encoding.ASCII.GetBytes(c.ToString())[0]);
    }

    public FontTexture(string fontId, byte c) : base(IoCManager.Resolve<IPrototypeManager>()
        .Index<FontPrototype>(fontId).Atlas)
    {
        _fontId = fontId;
        CreateTexture(c);
    }

    private void CreateTexture(byte c)
    {
        var proto = IoCManager.Resolve<IPrototypeManager>().Index<FontPrototype>(_fontId);
        var maxTextureWidth = Size.X / proto.Width;
        var y = (float)Math.Floor(c / maxTextureWidth);
        var x = (float)(16 - Math.Floor(((y + 1) * maxTextureWidth) - c)); // Need to revert that shit

        Rect = new UIBox2(
            new Vector2(x * proto.Width, y * proto.Height), 
            new Vector2((x * proto.Width) + proto.Width, (y * proto.Height) + proto.Height));
        Size = new Vector2(proto.Width, proto.Height);
    }
}