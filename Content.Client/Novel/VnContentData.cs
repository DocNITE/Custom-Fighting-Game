using Content.Client.Novel.Actions;

namespace Content.Client.Novel;

[DataDefinition]
public sealed partial class VnContentData
{
    [DataField] public string Text { get; set; } = string.Empty;
    [DataField] public float Delay { get; set; } = 70;

    [DataField] public List<IVnAction> Actions { get; set; } = new() { new VnActionDefault() };

    [DataField] public bool Skip { get; set; } = false;
    [DataField] public bool NewDialog { get; set; } = true;

    public float PassedTime;
}
