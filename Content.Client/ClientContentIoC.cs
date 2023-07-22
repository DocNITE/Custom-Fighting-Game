using Content.Client.UserInterfaces;

namespace Content.Client;

internal static class ClientContentIoC
{
    public static void Register()
    {
        IoCManager.Register<IGtkUserInterfaceManager, GtkUserInterfaceManager>();
    }
}