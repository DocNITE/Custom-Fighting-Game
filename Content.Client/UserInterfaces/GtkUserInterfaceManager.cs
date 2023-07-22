using Content.Client.UserInterfaces.Controls;

namespace Content.Client.UserInterfaces;

internal sealed class GtkUserInterfaceManager : IGtkUserInterfaceManager
{
    public GtkWidget RootScreen { get; private set; } = default!;

    public void Initialize()
    {
        RootScreen = new GtkWidget();
    }
}