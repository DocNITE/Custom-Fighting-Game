using System.Numerics;
using Content.Client.UserInterfaces;
using Content.Client.UserInterfaces.Controls;
using Content.Client.Novel;
using Robust.Shared.Timing;
using Robust.Shared.Input;
using Content.Shared.Input;
using Content.Client.Novel.Manager;

namespace Content.Client.GameMan.States;

public sealed class NovelState : GameState, INovelState
{
    public GtkNovelScreen Ui { get; } = new();

    public override void Initialize()
    {
        Viewport = new GtkNovelScreen();
        Viewport.AddChild(Ui);

        /*
         *         Ui.DoBackground("/Textures/Arts/vergen-tavern-file.png");
        var data = new VnContentData();
        data.Text = " Some text for animation test YEHOOO lol  AHHHHH lol xd ahahah xddd ahah its cool i think lol... Shit okay \n";
        Ui.DoBackgroundDialog(data);
        data = new VnContentData();
        data.Text = "new content yes... And it was coool lol! So, do you \n have some plans on tomorow? Maybe we try erp? FFFF 0123456789";
        data.Delay = 100;
        Ui.DoBackgroundDialog(data, false);
        */

        base.Initialize();

        IoCManager.Resolve<IVnSceneManager>().LoadScene("default");
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}

public class GtkNovelScreen : GtkWidget
{
    [Dependency] private readonly IVnSceneManager _vnSceneMan = default!;

    private VnContentData? _currentDialog { get; set; }

    public GtkTexture? Background;
    public GtkNovelDialogLabel? DialogLabel;

    public bool? IsMessage => DialogLabel?.IsMessage;

    public GtkNovelScreen()
    {
        Focus();
    }

    public void CreateDialog()
    {
        if (DialogLabel != null)
            DialogLabel.Dispose();

        DialogLabel = new GtkNovelDialogLabel();
        AddChild(DialogLabel);
    }

    public void CreateBackgroundDialog()
    {
        if (DialogLabel != null)
            DialogLabel.Dispose();

        DialogLabel = new GtkNovelBackgroundDialogLabel();
        AddChild(DialogLabel);
    }

    public void DoBackground(string? texPath)
    {
        if (Background != null)
            Background.Dispose();

        if (texPath == null)
            return;

        Background = new GtkTexture();
        AddChild(Background);
        Background.TexturePath = texPath;
        Background.Size = new Vector2(800, 600);
        Background.Visible = true;
    }

    public void DoDialog(VnContentData dialog, Vector2 pos, Vector2 size, bool newDialog = true)
    {
        if (newDialog == true || DialogLabel == null)
            CreateDialog();

        if (DialogLabel == null)
            return;

        DialogLabel.Label.FontScale = 1.0f;
        DialogLabel.Label.XPadding = 0.0f;
        DialogLabel.Label.YPadding = 15.0f;
        DialogLabel.Size = size;
        DialogLabel.Position = pos;
        DialogLabel.Visible = true;
        DialogLabel.MessageEnded += OnMessageEnded;
        DialogLabel.AddText(dialog);
    }

    public void DoBackgroundDialog(VnContentData dialog, bool newDialog = true)
    {
        if (newDialog == true || DialogLabel == null)
            CreateBackgroundDialog();

        if (DialogLabel == null)
            return;

        DialogLabel.Label.XPadding = 0.0f;
        DialogLabel.Label.YPadding = 15.0f;
        DialogLabel.Size = new Vector2(800, 600);
        DialogLabel.Position = Vector2.Zero;
        DialogLabel.Visible = true;
        DialogLabel.MessageEnded += OnMessageEnded;
        DialogLabel.AddText(dialog);
    }

    public void ClearDialog()
    {
        DialogLabel?.Dispose();
        DialogLabel = null;
    }

    public void SkipMessage()
    {
        Logger.Debug("PrSKIIIPdsse");

        if (DialogLabel == null)
            return;

        if (DialogLabel.IsMessage)
            DialogLabel.SpeedUpText();
        else
            Act();
    }

    public void Act()
    {
        if (_currentDialog == null) return;

        foreach (var action in _currentDialog.Actions)
        {
            action.Act();
        }

        _currentDialog = null;
    }

    public void OnMessageEnded(VnContentData dialog)
    {
        _currentDialog = dialog;

        if (dialog.Skip)
            SkipMessage();
    }

    public override void OnKeyBindDown(BoundKeyEventArgs args)
    {
        if (args.Function == EngineKeyFunctions.UIClick)
            SkipMessage();

        base.OnKeyBindDown(args);
    }

    public override bool OnDraw(GtkDrawingHandle handle)
    {
        if (!Visible)
            return false;

        return base.OnDraw(handle);
    }
}

public class GtkNovelLabel : GtkLabel
{
    private char[] _endSeperator = new char[] { '\u0009', '\u0010', '\u0011' };
    private float _delay = 200;
    private float _passedTime = 0;

    private int _currentSymbol = -1;

    private bool _isEnded = false;

    private readonly List<VnContentData> _messageQueue = new();

    public bool IsMessage => _messageQueue.Count > 0;
    public bool IsEnded
    {
        get => _isEnded;
    }

    public event Action<VnContentData>? MessageEnded;

    public void AddText(VnContentData dialogue)
    {
        _messageQueue.Add(dialogue);
        _isEnded = false;
    }

    public void SpeedUpText()
    {
        if (!IsMessage)
            return;

        _messageQueue[0].Delay = 10;
    }

    public override void FrameUpdate(FrameEventArgs args)
    {
        base.FrameUpdate(args);

        if (!IsMessage)
        {
            if (IsEnded)
            {
                _passedTime += args.DeltaSeconds * 1000;

                if (_passedTime >= _delay)
                {
                    _passedTime = 0;

                    if (this.Content.Length <= 0 || this.Content == string.Empty)
                        return;

                    if (_currentSymbol == -1)
                    {
                        _currentSymbol = 0;
                        this.Content += _endSeperator[_currentSymbol];
                    }

                    if (_currentSymbol > _endSeperator.Length - 1)
                        _currentSymbol = 0;

                    this.Content = this.Content.Substring(0, this.Content.Length - 1);
                    this.Content += _endSeperator[_currentSymbol];

                    _currentSymbol++;
                }
            }
            return;
        }

        if (_currentSymbol != -1)
        {
            this.Content = this.Content.Substring(0, this.Content.Length - 1);
            _currentSymbol = -1;
        }

        var currentDialog = _messageQueue[0];

        if (string.IsNullOrEmpty(currentDialog.Text))
        {
            _messageQueue.RemoveAt(0);
            OnMessageEnded(currentDialog);
            return;
        }

        currentDialog.PassedTime += args.DeltaSeconds * 1000;

        if (currentDialog.PassedTime >= currentDialog.Delay)
        {
            currentDialog.PassedTime = 0;

            this.Content += currentDialog.Text[0];
            currentDialog.Text = currentDialog.Text.Substring(1);
        }
    }

    private void OnMessageEnded(VnContentData dialog)
    {
        if (!IsMessage)
            _isEnded = true;

        MessageEnded?.Invoke(dialog);
    }
}

public class GtkNovelDialogLabel : GtkWindow
{
    public GtkNovelLabel Label = new();

    public bool IsEnded => Label.IsEnded;
    public bool IsMessage => Label.IsMessage;
    public event Action<VnContentData>? MessageEnded;

    public float Padding = 5.0f;

    public GtkNovelDialogLabel()
    {
        AddChild(Label);
        Label.MessageEnded += OnMessageEnded;
    }

    public void AddText(VnContentData dia)
    {
        Label.AddText(dia);
    }

    public void SpeedUpText()
    {
        Label.SpeedUpText();
    }

    public void OnMessageEnded(VnContentData dialog)
    {
        MessageEnded?.Invoke(dialog);
    }

    public override void InvalidateWidget()
    {
        base.InvalidateWidget();

        Label.Size = new Vector2(Size.X - Padding, Size.Y - Padding);
        Label.Position = new Vector2(Padding, Padding);
    }
}

public class GtkNovelBackgroundDialogLabel : GtkNovelDialogLabel
{
    public GtkNovelBackgroundDialogLabel() : base()
    {
        Padding = 20.0f;
        Label.FontScale = 1.5f;
        TexturePath = "/Textures/Interface/background-ui.png";
    }
}

