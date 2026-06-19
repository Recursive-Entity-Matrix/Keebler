using Dalamud.Interface.Windowing;
using Dalamud.Plugin;
using Keebler.Gui;

namespace Keebler;

public class Keebler : IDalamudPlugin
{
    private IDalamudPluginInterface _pluginInterface;
    private readonly Config _config;
    private readonly MainWindow _mainWindow;
    private readonly WindowSystem _windowSystem;
    
    public Keebler(IDalamudPluginInterface pluginInterface)
    {
        _pluginInterface = pluginInterface;
        _pluginInterface.Create<Services>();
        
        _config = Config.Load();
        _mainWindow = new MainWindow(_config);
        _windowSystem = new WindowSystem();
        _windowSystem.AddWindow(_mainWindow);
        Services.PluginInterface.UiBuilder.OpenMainUi += _mainWindow.Toggle;
        Services.PluginInterface.UiBuilder.Draw += _windowSystem.Draw;
    }

    public void Dispose()
    {
    }
}