using System.Numerics;
using System.Reflection;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.System.Input;
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

    private InputId _lastSelected = 0;
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

        if (!hotkeysHeld)
        {
            ImGui.SameLine();
            ImGui.TextDisabled("Hold Ctrl to enable buttons");
        }

        if (ImGuiUtil.GenericEnumCombo<InputId>("Add New Binding", 100f, _lastSelected, out var inputId))
        {
            _lastSelected = inputId;
            selectorCurrent.Keybinds.TryAdd(inputId, new Keybind());
            _config.Save();
        }
        
        using var child2 = ImRaii.Child("Keybinds", new Vector2(0, 0), true, ImGuiWindowFlags.None);
        foreach (var keybind in selectorCurrent.Keybinds)
        {
            using var id = ImRaii.PushId(keybind.Key.ToString());
            ImGui.Text(keybind.Key.ToString());
            ImGui.Indent();
            var bind = keybind.Value;
            if (DrawBinding(ref bind))
            {
                selectorCurrent.Keybinds[keybind.Key] = bind;
                _config.Save();
            }
            ImGui.Unindent();
            ImGui.Separator();
        }
    }

    private bool DrawBinding(ref Keybind bind)
    {
        var changed = false;
        ImGui.PushID("Key1");
        ImGui.Text("Key 1: ");
        ImGui.Indent();
        if (DrawKey(ref bind.KeySettings[0]))
        {
            changed = true;
        }
        ImGui.Unindent();
        ImGui.PopID();
        ImGui.PushID("Key2");
        ImGui.Text("Key 2: ");
        ImGui.Indent();
        if (DrawKey(ref bind.KeySettings[1]))
        {
            changed = true;
        }
        ImGui.Unindent();
        ImGui.PopID();
        ImGui.PushID("Game1");
        ImGui.Text("Gamepad 1: ");
        ImGui.Indent();
        if (DrawGamepad(ref bind.GamepadSettings[0]))
        {
            changed = true;
        }
        ImGui.Unindent();
        ImGui.PopID();
        ImGui.PushID("Game2");
        ImGui.Text("Gamepad 2: ");
        ImGui.Indent();
        if (DrawGamepad(ref bind.GamepadSettings[1]))
        {
            changed = true;
        }
        ImGui.Unindent();
        ImGui.PopID();
        return changed;
    }
    
    private bool DrawKey(ref KeySetting setting)
    {
        var changed = false;

        if (ImGuiUtil.GenericEnumCombo("Modifier", 150f, setting.KeyModifier, out KeyModifierFlag mod))
        {
            setting.KeyModifier = mod;
            changed = true;
        }

        ImGui.SameLine();

        if (ImGuiUtil.GenericEnumCombo("Key", 150f, setting.Key, out SeVirtualKey key))
        {
            setting.Key = key;
            changed = true;
        }

        return changed;
    }

    private bool DrawGamepad(ref KeySetting setting)
    {
        var changed = false;

        if (ImGuiUtil.GenericEnumCombo("Modifier", 150f, setting.GamepadModifier, out GamepadModifierFlag mod))
        {
            setting.GamepadModifier = mod;
            changed = true;
        }

        ImGui.SameLine();

        if (ImGuiUtil.GenericEnumCombo("Key", 150f, setting.Key, out SeVirtualKey key))
        {
            setting.Key = key;
            changed = true;
        }

        return changed;
    }
}