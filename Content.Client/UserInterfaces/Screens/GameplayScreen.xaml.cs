using System.Numerics;
using Content.Client.UserInterfaces.Controls;
using Robust.Client;
using Robust.Client.Animations;
using Robust.Client.AutoGenerated;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Animations;
using Robust.Shared.Configuration;
using Robust.Shared.ContentPack;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;

namespace Content.Client.UserInterfaces.Screens;

[GenerateTypedNameReferences, Virtual]
public partial class GameplayScreen : UIScreen
{
    [Dependency] private readonly IBaseClient _client = default!;
    [Dependency] private readonly IConfigurationManager _cfg = default!;
    [Dependency] private readonly IUserInterfaceManager _userInterface = default!;
    [Dependency] private readonly IClientNetManager _net = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly IResourceCache _resourceCache = default!;
    [Dependency] private readonly IResourceManager _resourceManager = default!;

    public GameplayScreen()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);

        AutoscaleMaxResolution = new Vector2i(1080, 770);

        SetAnchorPreset(MainViewport, LayoutPreset.Wide);
        SetAnchorPreset(ViewportContainer, LayoutPreset.Wide);

        
          // TODO: DEBUG
          /*
        var widget = new GtkWidget();
        IoCManager.Resolve<IGtkUserInterfaceManager>().RootScreen.AddChild(widget);
        widget.Position = new Vector2(0, 0);

        var text = new GtkLabel();
        text.Content = "  Ahah some cool text! Yeah, i know it...\n\n   Realy looks cool lol :3";
        text.Position = new Vector2(0, 0);
        text.Size = new Vector2(350, 200);
        text.FontScale = 2;
        widget.AddChild(text);
        
        var text2 = new GtkLabel();
        text2.Content = "Dafak?";
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
         */
    }
}
