using Content.Client.Animations;
using Robust.Client.Animations;
using Robust.Shared.Collections;
using Robust.Shared.Timing;
using static Content.Client.Animations.AnimationPlaybackShared;

namespace Content.Client.UserInterfaces.Controls;

public partial class GtkWidget
{
    private Dictionary<string, AnimationPlaybackShared.AnimationPlayback>? _playingAnimations;
    
    public Action<string>? AnimationCompleted;
    
    /// <summary>
    ///     Start playing an animation.
    /// </summary>
    /// <param name="animation">The animation to play.</param>
    /// <param name="key">
    ///     The key for this animation play. This key can be used to stop playback short later.
    /// </param>
    public void PlayAnimation(Animation animation, string key)
    {
        var playback = new AnimationPlaybackShared.AnimationPlayback(animation);
    
        _playingAnimations ??= new Dictionary<string, AnimationPlaybackShared.AnimationPlayback>();
        _playingAnimations.Add(key, playback);
    }
    
    public bool HasRunningAnimation(string key)
    {
        return _playingAnimations?.ContainsKey(key) ?? false;
    }
    
    public void StopAnimation(string key)
    {
        _playingAnimations?.Remove(key);
    }
    
    private void ProcessAnimations(FrameEventArgs args)
    {
        if (_playingAnimations == null || _playingAnimations.Count == 0)
        { 
            return;
        }
    
        var toRemove = new ValueList<string>();
    
        foreach (var (key, playback) in _playingAnimations)
        {
            var keep = UpdatePlayback(this, playback, args.DeltaSeconds);
            if (keep)
                continue;
    
            toRemove.Add(key);
        }
    
        foreach (var key in toRemove)
        {
            _playingAnimations.Remove(key);
            AnimationCompleted?.Invoke(key);
        }
    }
}