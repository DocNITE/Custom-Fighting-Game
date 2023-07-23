using System.Numerics;
using System.Text;
using Robust.Shared.Prototypes;

namespace Content.Client.Graphics.Fonts;

public enum FontStyle
{
    Box = 0,
    Normal = 1
}

public sealed class FontTexture : GraphicsTexture
{
    private readonly string _fontId;

    /// <summary>
    /// Sizing char style. Default is Box sizing
    /// </summary>
    public readonly FontStyle Style;
    
    public FontTexture(string fontId, char c, FontStyle style = FontStyle.Box) : base(IoCManager.Resolve<IPrototypeManager>()
        .Index<FontPrototype>(fontId).Atlas)
    {
        _fontId = fontId;
        Style = style;
        CreateTexture(Encoding.ASCII.GetBytes(c.ToString())[0]);
    }

    public FontTexture(string fontId, byte c, FontStyle style = FontStyle.Box) : base(IoCManager.Resolve<IPrototypeManager>()
        .Index<FontPrototype>(fontId).Atlas)
    {
        _fontId = fontId;
        Style = style;
        CreateTexture(c);
    }

    private void CreateTexture(byte c)
    {
        var proto = IoCManager.Resolve<IPrototypeManager>().Index<FontPrototype>(_fontId);
        var maxTextureWidth = Size.X / proto.Width;
        var y = (float)Math.Floor(c / maxTextureWidth);
        var x = (float)(16 - Math.Floor(((y + 1) * maxTextureWidth) - c));

        if (Style == FontStyle.Box)
        {
            Rect = new UIBox2(
                new Vector2(x * proto.Width, y * proto.Height), 
                new Vector2((x * proto.Width) + proto.Width, (y * proto.Height) + proto.Height));
            Size = new Vector2(proto.Width, proto.Height);
        }
        else if (Style == FontStyle.Normal)
        {
            var startPos = new Vector2(x * proto.Width, y * proto.Height);
            var endPos = new Vector2((x * proto.Width) + proto.Width, (y * proto.Height) + proto.Height);

            var startCut = (int)startPos.X;
            var endCut = (int)endPos.X;
            
            // for start cutting
            for (var zx = (int)startPos.X; zx < (int)endPos.X; zx++)
            {
                for (var zy = (int)startPos.Y; zy < (int)endPos.Y; zy++)
                {
                    var clr = Texture.GetPixel(zx, zy);
                    if (clr.AByte <= 0) continue;
                    if (zx <= startCut || startCut == (int)startPos.X)
                        startCut = zx;
                }
            }
            // for end cutting
            for (var zx = (int)startPos.X; zx < (int)endPos.X; zx++)
            {
                for (var zy = (int)startPos.Y; zy < (int)endPos.Y; zy++)
                {
                    var clr = Texture.GetPixel(zx, zy);
                    if (clr.AByte <= 0) continue;
                    if (zx >= endCut || endCut == (int)endPos.X)
                        endCut = zx;
                }
            }

            var newWidth = endCut - startCut + 2;
            
            Rect = new UIBox2(
                new Vector2(startCut-1, y * proto.Height), 
                new Vector2(endCut+1, (y * proto.Height) + proto.Height));
            Size = new Vector2(newWidth, proto.Height);
        }
    }
}