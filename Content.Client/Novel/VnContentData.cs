using Content.Client.Novel.Actions;

namespace Content.Client.Novel;

[DataDefinition]
public sealed partial class VnContentData
{
    [DataField("text")] public string Text { get; set; } = "...";
    [DataField("delay")] public float Delay { get; set; } = 70;
    [DataField("speed")] public float Speed { get; set; } = 1;

    [DataField("actions")] public List<IVnAction> Actions { get; set; } = new() { new VnActionDefault() };

    [DataField("skip")] public bool Skip { get; set; } = false;
    [DataField("newDialog")] public bool NewDialog { get; set; } = true;
}
