using Content.Client.UserInterfaces;

namespace Content.Client.IoC;

internal static class ClientContentIoC
{
    public static void Register()
    {
        IoCManager.Register<IGtkUserInterfaceManager, GtkUserInterfaceManager>();
    }
}