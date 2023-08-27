using Content.Shared.Input;
using Robust.Shared.Input;

namespace Content.Client.Input;

public static class ContentContexts
{
    public static void SetupContexts(IInputContextContainer contexts)
    {
        var common = contexts.New("world", "common");
        common.AddFunction(ContentKeyFunctions.OpenUi);
    }
}

