using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace Keebler;

public class Services
{
    [PluginService] public static IAddonLifecycle AddonLifecycle { get; set; } = null!;
    [PluginService] public static IChatGui ChatGui { get; set; } = null!;
    [PluginService] public static ICommandManager CommandManager { get; set; } = null!;
    [PluginService] public static IDalamudPluginInterface PluginInterface { get; set; } = null!;
    [PluginService] public static IPluginLog Log { get; set; } = null!;
    [PluginService] public static ICondition Condition { get; set; } = null!;
}