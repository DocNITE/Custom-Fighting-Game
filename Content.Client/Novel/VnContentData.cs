using Content.Client.Novel.Actions;

namespace Content.Client.Novel;

[DataDefinition]
public sealed class VnContentData
{
    [DataField("text")] public string Text { get; } = "...";
    [DataField("delay")] public float Delay { get; } = 70;
    [DataField("speed")] public float Speed { get; } = 1;

    [DataField("actions")] public List<IVnAction> Actions { get; } = new(){new VnActionDefault()}; 
    
    [DataField("skip")] public bool Skip { get; }
    [DataField("clear")] public bool Clear { get; } = true;
}