using JetBrains.Annotations;

namespace Content.Client.UserInterfaces.Controls;

[Virtual, PublicAPI]
public partial class GtkWidget : IDisposable
{
    private readonly List<GtkWidget> _orderedChildren = new();
    
    public IGtkUserInterfaceManager UserInterfaceManager { get; }
    
    public GtkWidget()
    {
        UserInterfaceManager = IoCManager.Resolve<IGtkUserInterfaceManager>();
    }
    
    public bool Disposed { get; private set; }
    
    public void Dispose()
    {
        Disposed = true;
    }
}