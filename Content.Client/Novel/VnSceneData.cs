namespace Content.Client.Novel;

[DataDefinition]
public sealed partial class VnSceneData
{
    [DataField] public string Id = "";
    [DataField] public List<VnContentData> Content = new();
}
