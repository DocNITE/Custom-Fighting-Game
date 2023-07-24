using System.Numerics;
using Content.Client.Graphics;
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
            InvalidateWidget();
        }
    }
    
    public Vector2 Position 
    { 
        get => _position;
        set
        {
            _position = value;
            InvalidateWidget();
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
        set
        {
            _size = value;
            InvalidateWidget();
        }
    }

    public IGtkUserInterfaceManager UserInterfaceManager { get; }
    
    public GtkWidget()
    {
        UserInterfaceManager = IoCManager.Resolve<IGtkUserInterfaceManager>();
        Children = _orderedChildren;
    }

    public virtual void Draw(GtkDrawingHandle handle)
    {
        DrawChilds(handle);
    }

    public virtual void DrawChilds(GtkDrawingHandle handle)
    {
        for (var i = 0; i < ChildCount; i++)
        {
            var widget = Children[0];
            widget.Draw(handle);
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
    }

    public void RemoveChild(GtkWidget child)
    {
        _orderedChildren.Remove(child);
        child.Parent = null;
    }

    private void InvalidateWidget()
    {
        _physicalPosition = Parent?.PhysicalPosition + Position ?? Position;
    }
    
    public bool Disposed { get; private set; }
    
    public void Dispose()
    {
        Disposed = true;
    }
}