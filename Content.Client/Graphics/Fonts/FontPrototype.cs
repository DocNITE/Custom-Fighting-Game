using Robust.Shared.Prototypes;

namespace Content.Client.Graphics.Fonts;

[Prototype("fontPrototype")]
public sealed class FontPrototype : IPrototype
{
    [IdDataField] public string ID { get; } = default!;
    
    /// <summary>
    /// Texture path for font atlas
    /// </summary>
    [DataField("atlas", required: true)] public string Atlas { get; } = default!;

    /// <summary>
    /// Single char width in pixels
    /// </summary>
    [DataField("width", required: true)] public int Width { get; } = default!;

    /// <summary>
    /// Single char height in pixels
    /// </summary>
    [DataField("height", required: true)] public int Height { get; } = default!;
}