using System.Numerics;
using Content.Client.UserInterfaces;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Animations;
using Robust.Client.Input;
using Robust.Shared.Animations;
using YamlDotNet.Core.Tokens;

namespace Content.Client.GameMan.States;

public class MenuState : GameState
{
    public override void Initialize()
    {
        Viewport = new GtkMenuScreen();
        var widget = Viewport;
        
        var text = new GtkLabel();
        text.Content = "  Ahah some cool text! Yeah, i know it...\n\n   Realy looks cool lol :3";
        text.Position = new Vector2(0, 0);
        text.Size = new Vector2(350, 200);
        text.FontScale = 2;
        widget.AddChild(text);
        
        var text2 = new GtkLabel();
        text2.Content = "  Da fak?";
        text2.Position = new Vector2(10, 0);
        text2.Size = new Vector2(350, 200);
        text2.FontScale = 2;
        widget.AddChild(text2);
        
        float _moveAniTime = 30f;
        
        var anim = new Animation
        {
            Length = TimeSpan.FromMilliseconds(_moveAniTime * 1000),
            AnimationTracks =
            {
                new AnimationTrackControlProperty
                {
                    Property = nameof(GtkLabel.Position),
                    InterpolationMode = AnimationInterpolationMode.Linear,
                    KeyFrames =
                    {
                        new AnimationTrackProperty.KeyFrame(new Vector2(0,0), 0f),
                        new AnimationTrackProperty.KeyFrame(new Vector2(300, 300), _moveAniTime)
                    }
                }
            }
        };
        text.PlayAnimation(anim, "lol");

        var texture = new GtkTexture();
        texture.Size = new Vector2(100, 100);
        texture.TexturePath = "/Textures/Arts/chel.png";
        widget.AddChild(texture);
        texture.ZIndex = -4;
        
        base.Initialize();
    }

    public override void OnInput(KeyEventArgs keyEvent, KeyEventType type)
    {
        base.OnInput(keyEvent, type);
        Logger.Debug($"{keyEvent.Key.ToString()}({type.ToString()})");
        if (keyEvent.Key == Keyboard.Key.W)
        {
            Logger.Debug("Works");
            Viewport.Children[0].Position =
                new Vector2(Viewport.Children[0].Position.X, Viewport.Children[0].Position.Y + 1);
        } 
    }
}

public class GtkMenuScreen : GtkWidget
{
    public override void Draw(GtkDrawingHandle handle)
    {
        base.Draw(handle);
    }
}

