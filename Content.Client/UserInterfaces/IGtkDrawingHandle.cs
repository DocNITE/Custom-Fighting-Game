using System.Numerics;
using Content.Client.Graphics;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces;

public partial interface IGtkDrawingHandle
{
    int CurrentRenderScale { get; }
    
    void DrawGlobalTexture(GraphicsTexture texture, Color? modulate = null);

    void DrawRectangle(UIBox2 rect, Color modulate, bool filled = false);
    
    void DrawRectangle(float fromX, float fromY, float toX, float toY, Color modulate, bool filled = false);

    void DrawLine(Vector2 from, Vector2 to, Color color);
    
    void DrawLine(float fromX, float fromY, float toX, float toY, Color color);

    void UseShader(ShaderInstance? shader);
}
