using System.Numerics;
using Robust.Client.Graphics;

namespace Content.Client.Graphics.Sprites;

[RegisterComponent]
public sealed class SpriteEntityComponent : Component
{
    [DataField("shader")] private string? shader;
    
    [DataField("sprite", readOnly: true)] private string? rsi;
    [DataField("layers", readOnly: true)] private List<PrototypeLayerData> layerDatums = new();

    [DataField("state", readOnly: true)] private string? state;
    [DataField("texture", readOnly: true)] private string? texture;
    [DataField("scale")] private Vector2? scale;
}