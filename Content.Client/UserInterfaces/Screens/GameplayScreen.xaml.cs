using Content.Client.Resources;
using Robust.Client;
using Robust.Client.AutoGenerated;
using Robust.Client.Graphics;
using Robust.Client.ResourceManagement;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Configuration;
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

    public GameplayScreen()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);

        AutoscaleMaxResolution = new Vector2i(1080, 770);

        SetAnchorPreset(MainViewport, LayoutPreset.Wide);
        SetAnchorPreset(ViewportContainer, LayoutPreset.Wide);
    }

    protected override void Draw(DrawingHandleScreen handle)
    {
        base.Draw(handle);

        //handle.UseShader(_prototypeManager.Index<ShaderPrototype>("CameraStatic").Instance());
        //handle.DrawTextureRect(_resourceCache.GetTexture("/Textures/Interface/Nano/square_black.png"), new UIBox2(new(0,0), this.Size), Color.White);
        //handle.DrawRect(new UIBox2(new(0,0), this.Size), Color.Black);
        //handle.UseShader(null);
    }
}
