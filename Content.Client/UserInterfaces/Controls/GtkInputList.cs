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
       
    }

    public void OnInput(KeyEventArgs keyEvent, KeyEventType type)
    {
        // TODO: Might be rework for InputManager or something
        if ((keyEvent.Key == Keyboard.Key.W || keyEvent.Key == Keyboard.Key.Up) && type == KeyEventType.Down)
        {
            var newVal = _focused++;
            //_focused = newVal >= List.Count ? 0 : newVal;
            Update();
        } else if ((keyEvent.Key == Keyboard.Key.S || keyEvent.Key == Keyboard.Key.Down) && type == KeyEventType.Down)
        {
            var newVal = _focused--;
            //_focused = newVal < 0 ? List.Count-1 : newVal;
            Update();
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