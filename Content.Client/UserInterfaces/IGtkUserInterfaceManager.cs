using Content.Client.UserInterfaces.Controls;

namespace Content.Client.UserInterfaces;

public interface IGtkUserInterfaceManager
{
    GtkWidget RootScreen { get; }

    void Initialize();
}
