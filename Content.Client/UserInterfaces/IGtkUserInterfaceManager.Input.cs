using Content.Client.Graphics.Viewport;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Graphics;
using Robust.Shared.Timing;

namespace Content.Client.UserInterfaces;

public partial interface IGtkUserInterfaceManager
{
    public GtkWidget? Focused { get; set; }
}
