namespace Content.Client.UserInterfaces.Controls;

public sealed class GtkButton : GtkWidget
{
    private readonly GtkLabel _label = new();
    private bool _focused = false;
    
    public string Bind { get; set; } = string.Empty;
    public string Name
    {
        get => _label.Content;
        set => _label.Content = value;
    }

    public bool Focused
    {
        get => _focused;
        set
        {
            _focused = value;
            _label.Color = _focused ? FocusColor : NormalColor;
        }
    }

    public float FontScale
    {
        get => _label.FontScale;
        set
        {
            _label.FontScale = value;
        }
    }

    public Color NormalColor = Color.White;
    public Color FocusColor = Color.Yellow;

    public GtkButton()
    {
        AddChild(_label);
    }
    
    public override void InvalidateWidget()
    {
        base.InvalidateWidget();

        _label.Size = Size;
    }
}