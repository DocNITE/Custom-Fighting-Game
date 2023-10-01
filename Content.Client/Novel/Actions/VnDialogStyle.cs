using Content.Client.Novel.Manager;

namespace Content.Client.Novel.Actions;

public sealed partial class VnDialogStyle : IVnAction
{
    [DataField] public DialogStyle Style = default!;

    public void Act()
    {
        // We try to continue dialog. Like with newDialog option 
        IoCManager.Resolve<IVnSceneManager>().DialogStyle = Style;
    }
}

