using Robust.Shared.Prototypes;

namespace Content.Client.Novel;

[Prototype("vnScene")]
public sealed class VnSceneData : IPrototype
{
    [IdDataField] public string ID { get; } = default!;
    
    [DataField("nextId")] public string? NextID { get; }
    [DataField("content")] public List<VnDialogData> Content { get; } = new();
}