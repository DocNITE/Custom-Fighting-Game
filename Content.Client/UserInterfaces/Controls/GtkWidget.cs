using System.Numerics;
using Content.Client.Graphics.Viewport;
using JetBrains.Annotations;
using Robust.Client.Graphics;

namespace Content.Client.UserInterfaces.Controls;

[Virtual, PublicAPI]
public partial class GtkWidget : IDisposable
{
    private readonly List<GtkWidget> _orderedChildren = new();
    private GtkWidget? _parent;

    private Vector2 _position = new Vector2(0, 0);
    private Vector2 _physicalPosition = new Vector2(0, 0);
    private Vector2 _size = new Vector2(0, 0);

    public List<GtkWidget> Children { get; }

    public int ChildCount => _orderedChildren.Count;
    
    public GtkWidget? Parent
    {
        get => _parent;
        private set
        {
            _parent = value;
        }
    }
    
    public Vector2 Position 
    { 
        get => _position;
        set
        {
            _physicalPosition = Parent?.PhysicalPosition ?? value;
            _position = value;
        }
    }
    
    public Vector2 PhysicalPosition 
    { 
        get => _physicalPosition;
        private set => _physicalPosition = value;
    }
    
    public Vector2 Size 
    { 
        get => _size;
        set => _size = value;
    }

    public IGtkUserInterfaceManager UserInterfaceManager { get; }
    
    public GtkWidget()
    {
        UserInterfaceManager = IoCManager.Resolve<IGtkUserInterfaceManager>();
        Children = _orderedChildren;
    }

    public virtual void Draw(GtkDrawingHandle handle)
    {
        
    }

    public void AddChild(GtkWidget child)
    {
        child.Parent?.RemoveChild(child);
        child.Parent = this;
        _orderedChildren.Add(child);
    }

    public void RemoveChild(GtkWidget child)
    {
        _orderedChildren.Remove(child);
        child.Parent = null;
    }
    
    public bool Disposed { get; private set; }
    
    public void Dispose()
    {
        Disposed = true;
    }
}