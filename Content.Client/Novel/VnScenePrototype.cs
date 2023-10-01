using Robust.Shared.Prototypes;

namespace Content.Client.Novel;

[Prototype("vnScene")]
public sealed partial class VnScenePrototype : IPrototype
{
    [IdDataField] public string ID { get; } = default!;
    //[DataField("objects")] public List<string> Characters { get; set; } = new();
    [DataField("content", required: true)] public List<VnContentData> Content = new();
}

