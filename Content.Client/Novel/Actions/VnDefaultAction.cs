using Content.Client.Novel.Manager;

namespace Content.Client.Novel.Actions;

public sealed partial class VnActionDefault : IVnAction
{
    public void Act()
    {
        // We try to continue dialog. Like with newDialog option 
        IoCManager.Resolve<IVnSceneManager>().Continue();
    }
}
