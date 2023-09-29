using Content.Client.UserInterfaces;
using Content.Client.UserInterfaces.Controls;

namespace Content.Client.GameMan.States;

public sealed class NovelState : GameState
{
    public override void Initialize()
    {
        Viewport = new GtkNovelScreen();
        
        base.Initialize();
    }
}

public class GtkNovelScreen : GtkWidget
{
    public GtkWindow TextWindow = new();
    public GtkLabel TextLabel = new();

    public GtkNovelScreen()
    {
        AddChild(TextWindow);
        AddChild(TextLabel);
    }
    
    public override bool OnDraw(GtkDrawingHandle handle)
    {
        return base.OnDraw(handle);
    }
}