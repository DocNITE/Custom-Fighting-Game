using Robust.Shared;
using Robust.Shared.Configuration;

namespace Content.Shared;

// DEVNOTE: This is the same as SS14's CCVars. Except it's not named CCVars as that name is 
// hot garbage.
[CVarDefs]
public sealed class ConfigGameVars : CVars
{
    public static readonly CVarDef<int> ViewportPhysicalWidth =
        CVarDef.Create("viewport.physical_width", 800, CVar.CLIENTONLY | CVar.ARCHIVE);
    
    public static readonly CVarDef<int> ViewportPhysicalHeight =
        CVarDef.Create("viewport.physical_height", 600, CVar.CLIENTONLY | CVar.ARCHIVE);
}