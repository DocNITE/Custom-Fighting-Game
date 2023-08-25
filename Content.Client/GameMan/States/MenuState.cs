using System.Numerics;
using Content.Client.UserInterfaces;
using Content.Client.UserInterfaces.Controls;
using Robust.Client.Animations;
using Robust.Client.GameObjects;
using Robust.Client.Input;
using Robust.Shared.Animations;

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

        var win = new GtkWindow();
        win.Size = new Vector2(780, 580);
        win.Position = new Vector2(10, 10);
        Viewport.AddChild(win);
        
        // TODO: Our content
        
        base.Initialize();
    }

    public override void OnInput(KeyEventArgs keyEvent, KeyEventType type)
    {
        base.OnInput(keyEvent, type);

        if (keyEvent.Key == Keyboard.Key.W)
        {
        } 
    }
}

public class GtkMenuScreen : GtkWidget
{
    public override void Draw(GtkDrawingHandle handle)
    {
        handle.DrawRectangle(0, 0, 800, 600, Color.Aqua, true);
        base.Draw(handle);
    }
}

