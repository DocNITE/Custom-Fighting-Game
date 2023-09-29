using System.Numerics;
using Content.Client.UserInterfaces;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Animations;
using Robust.Client.GameObjects;
using Robust.Client.Input;
using Robust.Shared.Animations;
using Robust.Shared.Input;

/*
public SoundSpecifier TryOpenDoorSound = new SoundPathSpecifier("/Audio/Effects/bang.ogg");
   public SoundSpecifier SparkSound = new SoundCollectionSpecifier("sparks");
   PlaySound(uid, door.SparkSound, AudioParams.Default.WithVolume(8), args.UserUid, false);
   SharedAudioSystem Audio
   protected override void PlaySound(EntityUid uid, SoundSpecifier soundSpecifier, AudioParams audioParams, EntityUid? predictingPlayer, bool predicted)
   {
   // If this sound would have been predicted by all clients, do not play any audio.
   if (predicted && predictingPlayer == null)
   return;
   
   if (predicted)
   Audio.PlayPredicted(soundSpecifier, uid, predictingPlayer, audioParams);
   else
   Audio.PlayPvs(soundSpecifier, uid, audioParams);
   }
*/

namespace Content.Client.GameMan.States;

public class MenuState : GameState
{
    public override void Initialize()
    {
        //IoCManager.Resolve<AudioSystem>().Play()
        // TODO: MusicSystem support, and make some title menu with Play, About, Exit
        // For fighting - we should use other fight states, and specifig 'FightingComponent' in entity, for DO their logic in game
        Viewport = new GtkMenuScreen();
        
        // add background content
        var someShit = new GtkTexture();
        someShit.TexturePath = "/Textures/Interface/main-screen-gradient.png";
        someShit.Position = new Vector2(400 - 256, 300 - 128);
        someShit.Color = new Color(255, 0, 0, 190);
        Viewport.AddChild(someShit);

        var txt = new GtkLabel();
        txt.Content = "Play\nNon play\nSomeFunny txt";
        txt.Size = new Vector2(158, 300);
        txt.Position = new Vector2(10, 10);
        Viewport.AddChild(txt);
        var win = new GtkInputList();
           win.Size = new Vector2(158, 300);
           win.Position = new Vector2(10, 10);
           win.AddButton("Play", "ev_play");
           win.AddButton("About", "ev_about");
           win.AddButton("Exit", "ev_exit");
           Viewport.AddChild(win);
         
        win.Focus();
        
        // TODO: MOSTLY IMPORTANT!!!
        // Need доделать GtkInputList. Нужно реализовать перемещение курсора и выбор кнопки.
        
        base.Initialize();
    }

    public override bool OnInput(BoundKeyEventArgs arg)
    {
        base.OnInput(arg);
        
        /*
         *  if (arg.Function == EngineKeyFunctions.MoveUp)
           Logger.Debug("Pressed");
         */

        return true;
    }
}

public class GtkMenuScreen : GtkWidget
{
    public override bool OnDraw(GtkDrawingHandle handle)
    {
        return base.OnDraw(handle);
    }
}

