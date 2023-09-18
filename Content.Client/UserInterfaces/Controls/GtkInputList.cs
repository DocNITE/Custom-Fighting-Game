using System.Numerics;
using Robust.Client.Input;
using Robust.Shared.Input;

namespace Content.Client.UserInterfaces.Controls;

public sealed class GtkInputList : GtkWindow
{
    private int _focused = -1;
    private float _padding = 10;
    private float _heightItemSize = 20;
    
    public Action<string>? OnFired;

    public void Update()
    {
        // TODO: Make focus support
        var yPos = 0.0f;
        foreach (var child in Children)
        {
            child.Position = new Vector2(_padding, yPos + _padding);
            yPos += _heightItemSize + _padding;
        }
    }

    private void Fire()
    {
        if (_focused == -1)
            return;
        
       // OnFired?.Invoke(List[_focused].Bind);
    }

    public void AddButton(string name, string bind)
    {
        var newButton = new GtkButton();
        newButton.Name = name;
        newButton.Bind = bind;
        newButton.Size = new Vector2(Size.X - (_padding*2), _heightItemSize);
        newButton.Position = new Vector2(_padding, _padding);
        newButton.FontScale = 1.5f;
        AddChild(newButton);
        Update();
    }

    public override void OnKeyBindDown(BoundKeyEventArgs args)
    {
        base.OnKeyBindDown(args);
        Logger.Debug(args.Function.FunctionName);
    }
}