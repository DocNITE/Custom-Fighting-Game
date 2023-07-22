using System.Numerics;
using Robust.Client.Graphics;
using Robust.Client.Serialization;
using Robust.Client.Utility;
using Robust.Shared.Utility;

namespace Content.Client.Graphics;

[Virtual]
public class GraphicsTexture
{
    private SpriteSpecifier.Texture? _frame;
    private SpriteSpecifier.Rsi? _rsi;
    private string? _state;
    private string? _path;
    private Direction _dir;

    private bool _isRsi = false;
    
    public Texture Texture { get; internal set; }
    
    public string? TexturePath
    {
        get => _path;
        set
        {
            _path = value;
            Frame = new SpriteSpecifier.Texture(new ResPath(_path ?? string.Empty));
        }
    }
    
    public SpriteSpecifier.Texture? Frame
    {
        get => _frame;
        internal set
        {
            _frame = value;
            if (_frame != null)
                Texture = _frame.DirFrame0().TextureFor(_dir);
        }
    }
    
    public SpriteSpecifier.Rsi? Rsi
    {
        get => _rsi;
        internal set
        {
            _rsi = value;
            if (_rsi != null)
                Texture = _rsi.DirFrame0().TextureFor(_dir);
        }
    }
    
    public Direction Direction
    {
        get => _dir;
        set
        {
            _dir = value;
            if (_frame != null)
                Texture = _frame.DirFrame0().TextureFor(_dir);
            else if (_rsi != null)
                Texture = _rsi.DirFrame0().TextureFor(_dir);
        }
    }

    public Vector2 Scale;
    public Vector2 Size;
    public UIBox2 Rect;
    public Vector2 Position;

    public string? State
    {
        get => _state;
        set
        {
            _state = value;
            Rsi = new SpriteSpecifier.Rsi(new ResPath(TexturePath ?? string.Empty), _state ?? string.Empty);
        }
    }

    public GraphicsTexture(string texPath, string rsiState = "")
    {
        _isRsi = rsiState != "";

        TexturePath = texPath;
        if (_isRsi)
        {
            State = rsiState;
            Rsi = new SpriteSpecifier.Rsi(new ResPath(TexturePath), State);
            Texture = Rsi.DirFrame0().Default;
            Size = Rsi.DirFrame0().Default.Size;
        }
        else
        {
            Frame = new SpriteSpecifier.Texture(new ResPath(TexturePath));
            Texture = Frame.DirFrame0().Default;
            Size = Frame.DirFrame0().Default.Size;
        }
        Direction = Direction.South;
        Scale = Vector2.One;
        Rect = new UIBox2(Vector2.Zero, Size);
        Position = Vector2.Zero;
    }
}