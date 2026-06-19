using System.Numerics;
using System.Reflection;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using OtterGui;
using OtterGui.Raii;

namespace Keebler.Gui;

public class MainWindow : Window
{
    private readonly ProfileSelector _selector;
    private readonly Config _config;
    public MainWindow(Config config) : base("Keebler " + Assembly.GetExecutingAssembly().GetName().Version, ImGuiWindowFlags.None)
    {
        _config = config;
        _selector = new ProfileSelector(_config);
    }

    public override void Draw()
    {
        _selector.Draw(ImGui.GetContentRegionAvail().X * 0.25f);
        ImGui.SameLine();
        if (_selector.Current == null)
        {
            ImGui.Text("No profile selected");
        }
        else
        {
            DrawProfile(_selector.Current);
        }
    }

    private void DrawProfile(KeybindProfile selectorCurrent)
    {
        using var child = ImRaii.Child("Profile", new Vector2(0, 0), true, ImGuiWindowFlags.None);
        var hotkeysHeld = ImGui.IsKeyDown(ImGuiKey.LeftCtrl) || ImGui.IsKeyDown(ImGuiKey.RightCtrl);
        if (ImGuiUtil.DrawDisabledButton("Apply to game", new Vector2(0, 0),"Apply the profile to your current in-game settings", !hotkeysHeld))
        {
            selectorCurrent.SetKeybinds();
        }
        ImGui.SameLine();
        if (ImGuiUtil.DrawDisabledButton("Populate from game", new Vector2(0, 0),"Populate the profile using your current in-game settings", !hotkeysHeld))
        {
            selectorCurrent.PopulateFromGame();
            _config.Save();
        }
        foreach (var bind in selectorCurrent.Keybinds)
        {
            ImGui.Text(bind.Key.ToString());
            ImGui.Text("Keyboard:");
            ImGui.Indent();
            foreach (var keybind in bind.Value.KeySettings)
            {
                ImGui.Text($"{keybind.Key} {keybind.KeyModifier}");
            }
            ImGui.Unindent();
            ImGui.Text("Gamepad:");
            ImGui.Indent();
            foreach (var controllerBind in bind.Value.GamepadSettings)
            {
                ImGui.Text($"{controllerBind.Key} {controllerBind.KeyModifier}");
            }
            ImGui.Unindent();
            ImGui.Separator();
        }
    }
}