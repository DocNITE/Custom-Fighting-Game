using System.Numerics;
using Content.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

[Virtual]
public partial class GtkWindow : GtkWidget
{
    private float _textureSize = 32;

    public string TexturePath { get; set; } = "/Textures/Interface/win-gui.png";
    
    public override void OnDraw(GtkDrawingHandle handle)
    {
        // Draw sliced window image
        DrawSliceImage(handle);
        
        base.OnDraw(handle);
    }

    #region Drawing methods
    protected virtual void DrawSliceImage(GtkDrawingHandle handle)
    {
        for (var i = 1; i <= 9; i++)
        {
            var localSize = new Vector2(_textureSize/3, _textureSize/3);
            var tex = new GraphicsTexture(TexturePath);
            
            localSize = new Vector2(_textureSize/3, _textureSize/3);
            if (Size.X <= _textureSize)
                localSize = new Vector2(Size.X/3, localSize.Y);
            if (Size.Y <= _textureSize)
                localSize = new Vector2(localSize.X, Size.Y/3);
            
            switch (i)
            {
                case 1:
                    tex.Rect = new UIBox2(Vector2.Zero, localSize);
                    tex.Size = localSize;
                    tex.Position = Vector2.Zero;
                    
                    DrawTexture(handle, tex);
                    break;
                case 2:
                    tex.Rect = new UIBox2(new Vector2(localSize.X, 0), new Vector2(localSize.X*2, localSize.Y));
                    tex.Size = new Vector2(Size.X - ((localSize.X)*2), localSize.Y);
                    tex.Position = new Vector2(localSize.X, 0);
                    
                    DrawTexture(handle, tex);
                    break;
                case 3:
                    tex.Rect = new UIBox2(new Vector2(_textureSize - localSize.X, 0), new Vector2(_textureSize, localSize.Y));
                    tex.Size = localSize;
                    tex.Position = new Vector2(Size.X - localSize.X, 0);
                    
                    DrawTexture(handle, tex);
                    break;
                case 4:
                    tex.Rect = new UIBox2(new Vector2(0, localSize.Y), new Vector2(localSize.X, localSize.Y*2));
                    tex.Size = new Vector2(localSize.X, Size.Y - (localSize.Y * 2));
                    tex.Position = new Vector2(0, localSize.Y-1);
                    
                    DrawTexture(handle, tex);
                    break;
                case 5:
                    tex.Rect = new UIBox2(localSize, new Vector2(_textureSize, _textureSize) - localSize);
                    tex.Size = new Vector2(Size.X - (localSize.X*2), Size.Y - (localSize.Y*2));
                    tex.Position = localSize - new Vector2(0, 1);
                    
                    DrawTexture(handle, tex);
                    break;
                case 6:
                    tex.Rect = new UIBox2(new Vector2(_textureSize - localSize.X, localSize.Y), new Vector2(_textureSize, localSize.Y*2));
                    tex.Size = new Vector2(localSize.X, Size.Y - (localSize.Y * 2));;
                    tex.Position = new Vector2(Size.X - localSize.X, localSize.Y-1);
                    
                    DrawTexture(handle, tex);
                    break;
                case 7:
                    tex.Rect = new UIBox2(new Vector2(0, localSize.Y*2), new Vector2(localSize.X, _textureSize));
                    tex.Size = localSize;
                    tex.Position = new Vector2(0, Size.Y - localSize.Y-1);
                    
                    DrawTexture(handle, tex);
                    break;
                case 8:
                    tex.Rect = new UIBox2(new Vector2(localSize.X, localSize.Y*2), new Vector2(localSize.X*2, _textureSize));
                    tex.Size = new Vector2(Size.X - ((localSize.X)*2), localSize.Y);
                    tex.Position = new Vector2(localSize.X, Size.Y - localSize.Y-1);
                    
                    DrawTexture(handle, tex);
                    break;
                case 9:
                    tex.Rect = new UIBox2(new Vector2(_textureSize - localSize.X, localSize.Y*2), new Vector2(_textureSize, _textureSize));
                    tex.Size = localSize;
                    tex.Position = new Vector2(Size.X - localSize.X, Size.Y - localSize.Y-1);
                    
                    DrawTexture(handle, tex);
                    break;
            }
        }
    }
    #endregion
}