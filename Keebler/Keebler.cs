using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Keebler.Gui;

namespace Keebler;

public class Keebler : IDalamudPlugin
{
    private IDalamudPluginInterface _pluginInterface;
    private readonly Config _config;
    private readonly MainWindow _mainWindow;
    private readonly ConfigWindow _configWindow;
    private readonly WindowSystem _windowSystem;

    public Keebler(IDalamudPluginInterface pluginInterface)
    {
        _pluginInterface = pluginInterface;
        _pluginInterface.Create<Services>();

        _config = Config.Load();

        _configWindow = new ConfigWindow(_config);
        _mainWindow = new MainWindow(_config, _configWindow);
        _windowSystem = new WindowSystem();
        _windowSystem.AddWindow(_mainWindow);
        _windowSystem.AddWindow(_configWindow);

        Services.PluginInterface.UiBuilder.OpenMainUi += _mainWindow.Toggle;
        Services.PluginInterface.UiBuilder.OpenConfigUi += _configWindow.Toggle;
        Services.PluginInterface.UiBuilder.Draw += _windowSystem.Draw;

        Services.CommandManager.AddHandler("/keebler", new CommandInfo(OnMainCommand)
        {
            HelpMessage = "Use '/keebler help' for more info"
        });
    }

    private void OnMainCommand(string command, string arguments)
    {
        if (string.IsNullOrEmpty(arguments))
        {
            _mainWindow.Toggle();
            return;
        }

        var args = arguments.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        switch (args[0].ToLower())
        {
            case "help":
            {
                Services.ChatGui.Print("Available commands:");
                Services.ChatGui.Print("/keebler - Open the main window");
                Services.ChatGui.Print("/keebler help => Show this help message");
                Services.ChatGui.Print("/keebler config => Open the config window");
                Services.ChatGui.Print("/keebler apply (profile name) => Apply a profile");
                Services.ChatGui.Print("If multiple profiles have the same name, the first one will be applied. Partial names are allowed. Case insensitive.");
                break;
            }
            case "config":
            {
                _configWindow.Toggle();
                break;
            }
            case "apply":
            {
                var name = args[1];
                if (string.IsNullOrWhiteSpace(name))
                {
                    Services.ChatGui.PrintError("Please provide a profile name to apply");
                    return;
                }
                var profile = _config.KeybindProfiles.FirstOrDefault(p => p.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
                if (profile == null)
                {
                    Services.ChatGui.PrintError($"Profile '{name}' not found");
                    return;
                }
                profile.SetKeybinds();
                if (_config.FeedbackOnSuccess)
                {
                    Services.ChatGui.Print($"Applied profile '{profile.Name}'");
                }
                break;
            }
            default:
                Services.ChatGui.Print($"Unknown command: {args[0]}");
                break;
        }
    }


    public void Dispose()
    {
        Services.PluginInterface.UiBuilder.OpenMainUi -= _mainWindow.Toggle;
        Services.PluginInterface.UiBuilder.OpenConfigUi -= _configWindow.Toggle;
        Services.PluginInterface.UiBuilder.Draw -= _windowSystem.Draw;
    }
}