using Content.Client.UserInterfaces.Controls;
using Robust.Shared.Input;

namespace Content.Client.UserInterfaces;

internal sealed partial class GtkUserInterfaceManager
{
    private GtkWidget? _focused;
    public GtkWidget? Focused
    {
        get => _focused;
        set
        {
            if (_focused == value)
                return;
            _focused?.ControlFocusExited();
            _focused = value;
        }
    }
    
    private bool OnUIKeyBindStateChanged(BoundKeyEventArgs args)
    {
        Focused?.OnKeyBind(args);
        
        if (args.State == BoundKeyState.Down)
        {
            KeyBindDown(args);
        }
        else
        {
            KeyBindUp(args);
        }

        // If we are in a focused control or doing a CanFocus, return true
        // So that InputManager doesn't propagate events to simulation.
        if (!args.CanFocus && Focused != null)
        {
            return true;
        }

        return false;
    }

    private void KeyBindDown(BoundKeyEventArgs args)
    {
        Focused?.OnKeyBindDown(args);
    }
    
    private void KeyBindUp(BoundKeyEventArgs args)
    {
        Focused?.OnKeyBindUp(args);
    }
}