using System.Linq;
using System.Numerics;
using Content.Client.Graphics;
using Content.Client.Graphics.Viewport;
using JetBrains.Annotations;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Shared.Animations;
using Robust.Shared.Input;
using Robust.Shared.Timing;

namespace Content.Client.UserInterfaces.Controls;

//TODO: Need make input system for gtkWidget
[Virtual, PublicAPI]
public partial class GtkWidget : IDisposable
{
    private readonly List<GtkWidget> _orderedChildren = new();
    private GtkWidget? _parent;

    private Vector2 _position = new Vector2(0, 0);
    private Vector2 _physicalPosition = new Vector2(0, 0);
    private Vector2 _size = new Vector2(0, 0);

    private int _zindex = 0;

    public List<GtkWidget> Children { get; private set; }

    public int ChildCount => _orderedChildren.Count;
    
    public GtkWidget? Parent
    {
        get => _parent;
        private set
        {
            _parent = value;
            InvalidateWidget();
        }
    }
    
    [Animatable]
    public Vector2 Position 
    { 
        get => _position;
        set
        {
            _position = value;
            InvalidateWidget();
        }
    }
    
    [Animatable]
    public Vector2 PhysicalPosition 
    { 
        get => _physicalPosition;
        private set => _physicalPosition = value;
    }
    
    [Animatable]
    public Vector2 Size 
    { 
        get => _size;
        set
        {
            _size = value;
            InvalidateWidget();
        }
    }

    // FIXME: It order parent child only when he HAS parent
    public int ZIndex
    {
        get => _zindex;
        set
        {
            _zindex = value;
            Parent?.OrderChilds();
            InvalidateWidget();
        }
    }
    
    public bool Visible { get; set; } = true; 

    public IGtkUserInterfaceManager UserInterfaceManager { get; }
    
    public GtkWidget()
    {
        UserInterfaceManager = IoCManager.Resolve<IGtkUserInterfaceManager>();
        Children = _orderedChildren;
    }

    public void Focus()
    {
        UserInterfaceManager.Focused = this;
    }

    public virtual void OnDraw(GtkDrawingHandle handle)
    {
        if (!Visible)
            return;
        
        DrawChilds(handle);
    }

    public virtual void FrameUpdate(FrameEventArgs args)
    {
        ProcessAnimations(args);
    }

    public virtual void ControlFocusExited()
    {
    }

    public virtual void OnKeyBindDown(BoundKeyEventArgs args)
    {
    }

    public virtual void OnKeyBindUp(BoundKeyEventArgs args)
    {
    }

    public virtual void OnKeyBind(BoundKeyEventArgs args)
    {
    }
    
    internal int DoFrameUpdateRecursive(FrameEventArgs args)
    {
        var total = 1;
        FrameUpdate(args);

        foreach (var child in Children)
        {
            total += child.DoFrameUpdateRecursive(args);
        }

        return total;
    }

    public virtual void DrawChilds(GtkDrawingHandle handle)
    {
        for (var i = 0; i < ChildCount; i++)
        {
            var widget = Children[i];
            widget.OnDraw(handle);
        }
    }

    protected virtual void DrawTexture(GtkDrawingHandle handle, GraphicsTexture texture, Color? modulate = null)
    {
        texture.Position += PhysicalPosition;
        handle.DrawGlobalTexture(texture, modulate);
    }

    public void AddChild(GtkWidget child)
    {
        child.Parent?.RemoveChild(child);
        child.Parent = this;
        _orderedChildren.Add(child);
        // FIXME: We might be use there OrderChilds()
        InvalidateWidget();
    }

    public void RemoveChild(GtkWidget child)
    {
        _orderedChildren.Remove(child);
        child.Parent = null;
        // FIXME: We might be use there OrderChilds()
        InvalidateWidget();
    }

    public virtual void InvalidateWidget()
    {
        _physicalPosition = (Parent?.PhysicalPosition + Position ?? Position) * UserInterfaceManager.CurrentRenderScale;
    }
    
    public bool Disposed { get; private set; }
    
    public void Dispose()
    {
        if (Disposed)
            return;
        
        Disposed = true;
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
            return;
        
        foreach (var child in Children)
        {
            child.Parent = null;
        }
        
        DisposeAllChildren();
        Parent?.RemoveChild(this);
    }
    
    public void DisposeAllChildren()
    {
        foreach (var child in Children)
        {
            child.Dispose();
        }
    }
    
    public void RemoveAllChildren()
    {
        foreach (var child in Children)
        {
            RemoveChild(child);
        }
    }
    
    public void Orphan()
    {
        Parent?.RemoveChild(this);
    }

    public void OrderChilds()
    {
        Children = Children.OrderBy(widget => widget.ZIndex).ToList();
    }
}