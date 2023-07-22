using System.Numerics;
using Robust.Client.Graphics;
using Robust.Client.Serialization;
using Robust.Client.Utility;
using Robust.Shared.Utility;

namespace Content.Client.Graphics;

[Virtual]
public class GraphicsTexture
{
    private string? _path;
    private Direction _dir;

    public Texture Texture { get; private set; }
    
    public string? TexturePath
    {
        get => _path;
        init
        {
            _path = value;
            Frame = new SpriteSpecifier.Texture(new ResPath(_path ?? string.Empty));
        }
    }
    
    public SpriteSpecifier.Texture? Frame { get; internal set; }

    public SpriteSpecifier.Rsi? Rsi { get; internal init; }

    public Direction Direction
    {
        get => _dir;
        set
        {
            _dir = value;
            if (Rsi != null)
                Texture = Rsi.DirFrame0().TextureFor(_dir);
        }
    }

    public Vector2 Scale;
    public Vector2 Size;
    public UIBox2 Rect;
    public Vector2 Position;

    public string? State
    {
        get => Rsi?.RsiState;
    }

    public GraphicsTexture(string texPath, string rsiState = "")
    {
        var isRsi = rsiState != "";

        TexturePath = texPath;
        
        if (isRsi)
        {
            Rsi = new SpriteSpecifier.Rsi(new ResPath(TexturePath), rsiState);
            Texture = Rsi.DirFrame0().TextureFor(0);
        }
        else
        {
            Frame = new SpriteSpecifier.Texture(new ResPath(TexturePath));
            Texture = Frame.DirFrame0().Default;
        }
        
        Size = Texture.Size;
        Direction = Direction.South;
        Scale = Vector2.One;
        Rect = new UIBox2(Vector2.Zero, Size);
        Position = Vector2.Zero;
    }
}