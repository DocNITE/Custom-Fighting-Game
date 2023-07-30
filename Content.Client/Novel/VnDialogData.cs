using Content.Client.Novel.Actions;

namespace Content.Client.Novel;

[DataDefinition]
public sealed class VnDialogData
{
    [DataField("id")] public string Id { get; } = "start";
    [DataField("nextId")] public string? NextId { get; }
    
    [DataField("text")] public string Text { get; } = "...";
    [DataField("delay")] public float Delay { get; } = 70;
    
    [DataField("background")] public string? Background { get; }
    [DataField("fade")] public float? Fade { get; }

    [DataField("actions")] public List<IVnAction> Actions { get; } = new(){new VnActionDefault()}; 
    
    [DataField("skip")] public bool Skip { get; }
}