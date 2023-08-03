using Content.Client.Input;
using Content.Client.Novel.Manager;
using Content.Client.UserInterfaces;

namespace Content.Client.IoC;

internal static class ClientContentIoC
{
    public static void Register()
    {
        IoCManager.Register<IGtkUserInterfaceManager, GtkUserInterfaceManager>();
        IoCManager.Register<IVnSceneManager, VnSceneManager>();
        IoCManager.Register<InputHookupManager>();
    }
}