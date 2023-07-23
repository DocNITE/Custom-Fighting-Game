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
        // Encoding.ASCII.GetBytes(c.ToString())[0]
    }

    public FontTexture(string fontId, byte c) : base(IoCManager.Resolve<IPrototypeManager>()
        .Index<FontPrototype>(fontId).Atlas)
    {
        _fontId = fontId;
        // TODO: Make for byte type too
    }

    private void CreateTexture(byte c)
    {
        var proto = IoCManager.Resolve<IPrototypeManager>().Index<FontPrototype>(_fontId);
        // get max width - Size.X/FontPrototype.Width = (16)
        // get Y - byte/max width = (y char position)
        // get X - ( (y char position) + 1 ) - byte = (x char position)
    }
}