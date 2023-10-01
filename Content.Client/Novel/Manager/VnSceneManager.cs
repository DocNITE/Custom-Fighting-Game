using System.Numerics;
using Content.Client.GameMan;
using Content.Client.GameMan.States;
using Robust.Client;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager;

namespace Content.Client.Novel.Manager;

public sealed class VnSceneManager : IVnSceneManager
{
    [Dependency] private readonly IPrototypeManager _protoManager = default!;
    [Dependency] private readonly IGameMan _gameMan = default!;
    [Dependency] private readonly IGameController _gameController = default!;
    [Dependency] private readonly ISerializationManager _serialMan = default!;

    private ISawmill _logger = default!;

    public VnScenePrototype? CurrentScene { get; set; }
    public DialogStyle DialogStyle { get; set; } = DialogStyle.Default;

    public void Initialize()
    {
        _logger = Logger.GetSawmill("vnSceneManager");
        IoCManager.InjectDependencies(this);
    }

    public void LoadScene(string prototype)
    {
        CleanupScene();

        if (!_protoManager.TryIndex<VnScenePrototype>(prototype, out var proto))
        {
            //_cfg.SetCVar("game.last_scene", "default");
            throw new Exception($"Scene {prototype} not found!");
        }

        //CurrentScene = _serialMan.CreateCopy(proto, nonNullableOverride: true);
        CurrentScene = proto;

        Continue();
    }

    public void Continue()
    {
        var ui = GetStateGui();
        var label = ui?.DialogLabel;

        if (CurrentScene == null)
            return;

        if (CurrentScene.Content.Count == 0)
        {
            //TODO: MAKE ENDING UI!
            //TODO2: Also, shutdown must be optionaly! We should swith game state from
            // NovelState to MenuState, i mean we back into main menu
            // Maybe we should add locale support?
            _gameController.Shutdown("The story is end...");
            return;
        }

        if (label is not null && label.IsMessage)
            return;

        var currentDialog = CurrentScene.Content[0];
        //DoActions(currentDialog);
        if (string.IsNullOrEmpty(currentDialog.Text)) currentDialog.Skip = true;
        DoDialog(currentDialog);

        CurrentScene.Content.RemoveAt(0);
    }

    public void CleanupScene()
    {
        var ui = GetStateGui();
        if (ui is not null)
            ui.ClearDialog();
        CurrentScene = null;
    }

    /// <summary>
    /// Take main UI screen from NovelState
    /// </summary>
    public GtkNovelScreen? GetStateGui()
    {
        var gameMan = IoCManager.Resolve<IGameMan>();
        if (gameMan.TargetState == null)
            return null;
        var state = (NovelState)gameMan.TargetState;
        return state != null ? state.Ui : null;
    }

    /// <summary>
    /// Execute all actions in current dialog
    /// </summary>
    private void DoActions(VnContentData dialog)
    {
        foreach (var action in dialog.Actions)
        {
            action.Act();
        }
    }

    /// <summary>
    /// Put a new dialog on screen. We use that method for aply VnScene's DialogStyle
    /// </summary>
    private void DoDialog(VnContentData dialog)
    {
        var ui = GetStateGui();
        var dialogWinSize = new Vector2(300, 150);
        // Why i make shit code?
        var newDialog = dialog.NewDialog;

        if (ui == null)
            return;

        switch (DialogStyle)
        {
            case DialogStyle.Default:
                ui.DoBackgroundDialog(dialog, newDialog);
                break;
            case DialogStyle.CharacterLeft:
                ui.DoDialog(dialog, new Vector2(20, 20), dialogWinSize, newDialog);
                break;
            case DialogStyle.CharacterRight:
                ui.DoDialog(dialog, new Vector2(800 - dialogWinSize.X - 20, 20), dialogWinSize, newDialog);
                break;
            case DialogStyle.CharacterCenter:
                ui.DoDialog(dialog, new Vector2(400 - (dialogWinSize.X / 2), 20), dialogWinSize, newDialog);
                break;
            case DialogStyle.Author:
                break;
        }
    }
}

//TODO Надо придумать, как взаимодействовать с UI из NovelState через акшены и VnSceneManager
