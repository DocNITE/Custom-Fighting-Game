using Robust.Client.Graphics;

namespace Content.Client.Entity.Sprite;

[RegisterComponent]
public sealed class EntSpriteComponent : Component
{
    [DataField("sprite", readOnly: true)] private string? rsi;
    [DataField("layers", readOnly: true)] private List<PrototypeLayerData> layerDatums = new();

    [DataField("state", readOnly: true)] private string? state;
    [DataField("texture", readOnly: true)] private string? texture;
}