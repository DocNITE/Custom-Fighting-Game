using Content.Client.Graphics;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces;

public partial interface IGtkDrawingHandle
{
    int CurrentRenderScale { get; }
    
    void DrawGlobalTexture(GraphicsTexture texture, Color? modulate = null);

    void UseShader(ShaderInstance? shader);
}
