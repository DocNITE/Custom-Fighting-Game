using Robust.Shared.Prototypes;

namespace Content.Client.Novel;

[Prototype("vnScene")]
public sealed partial class VnSceneData : IPrototype
{
    [IdDataField] public string ID { get; } = default!;
    [DataField("characters")] public List<string> Characters { get; } = new();
    [DataField("content")] public List<VnContentData> Content { get; } = new();
}
