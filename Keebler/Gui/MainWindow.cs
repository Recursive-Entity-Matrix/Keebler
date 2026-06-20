using System.Numerics;
using System.Reflection;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.System.Input;
using Keebler.Models;
using OtterGui;
using OtterGui.Raii;

namespace Keebler.Gui;

public class MainWindow : Window
{
    private readonly ProfileSelector _selector;
    private readonly Config _config;
    private readonly ConfigWindow _configWindow;

    public MainWindow(Config config, ConfigWindow configWindow) : base("Keebler " + Assembly.GetExecutingAssembly().GetName().Version,
        ImGuiWindowFlags.None)
    {
        _config = config;
        _configWindow = configWindow;
        _selector = new ProfileSelector(_config);
        Size = new Vector2(600, 400);
        SizeCondition = ImGuiCond.FirstUseEver;
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(400, 300)
        };
        TitleBarButtons.Add(new TitleBarButton()
        {
            Click = click => _configWindow.Toggle(),
            Icon = FontAwesomeIcon.Cog,
            ShowTooltip = () =>
            {
                ImGui.BeginTooltip();
                ImGui.Text("Open Configuration Window");
                ImGui.EndTooltip();
            },
        });
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

    private InputId _selectedAddBinding = 0;
    private InputId? _selectedRemoveBinding = null;

    private void DrawProfile(KeybindProfile selectorCurrent)
    {
        using var child = ImRaii.Child("Profile", new Vector2(0, 0), true, ImGuiWindowFlags.None);

        var name = selectorCurrent.Name;
        if (ImGui.InputText("Name", ref name, 100))
        {
            selectorCurrent.Name = name;
            _config.Save();
        }

        var description = selectorCurrent.Description;
        if (ImGui.InputTextMultiline("Description", ref description, 1000, new Vector2(0, 60)))
        {
            selectorCurrent.Description = description;
            _config.Save();
        }

        if (ImGuiUtil.GenericEnumCombo("Game Action", 350f, _selectedAddBinding, out var inputId,
                id => id.ToString().MakePretty()))
        {
            _selectedAddBinding = inputId;
        }

        if (ImGui.Button("Add/Reset Binding", new Vector2(0, 0)))
        {
            selectorCurrent.Keybinds[_selectedAddBinding] = new KeybindDto();
            _config.Save();
            _selectedAddBinding = 0;
        }

        ImGui.SameLine();
        var hotkeysHeld = ImGui.IsKeyDown(ImGuiKey.LeftCtrl) || ImGui.IsKeyDown(ImGuiKey.RightCtrl);
        if (ImGuiUtil.DrawDisabledButton("Apply to game", new Vector2(0, 0),
                "Apply the profile to your current in-game settings", !hotkeysHeld))
        {
            selectorCurrent.SetKeybinds();
            if (_config.FeedbackOnSuccess)
            {
                Services.ChatGui.Print($"Applied profile '{selectorCurrent.Name}'");
            }
        }

        ImGui.SameLine();
        if (ImGuiUtil.DrawDisabledButton("Populate from game", new Vector2(0, 0),
                "Populate the profile using your current in-game settings", !hotkeysHeld))
        {
            selectorCurrent.PopulateFromGame();
            _config.Save();
        }

        if (!hotkeysHeld)
        {
            ImGui.SameLine();
            ImGui.TextDisabled("Hold Ctrl to enable buttons");
        }

        using (ImRaii.Child("Keybinds", new Vector2(0, 0), true, ImGuiWindowFlags.None))
        {
            foreach (var (key, bind) in selectorCurrent.Keybinds)
            {
                using var id = ImRaii.PushId(key.ToString());
                ImGui.Text(key.ToString().MakePretty());
                ImGui.Indent();
                if (DrawBinding(bind))
                {
                    selectorCurrent.Keybinds[key] = bind;
                    _config.Save();
                }

                ImGui.Unindent();
                
                using (ImRaii.PushFont(UiBuilder.IconFont))
                {
                    if (ImGui.Button(FontAwesomeIcon.Trash.ToIconString(), new Vector2(0, 0)))
                    {
                        _selectedRemoveBinding = key;
                    }
                }
                ImGuiUtil.HoverTooltip("Remove this keybind");

                ImGui.Separator();
            }
        }

        if (_selectedRemoveBinding.HasValue)
        {
            selectorCurrent.Keybinds.Remove(_selectedRemoveBinding.Value);
            _config.Save();
            _selectedRemoveBinding = null;
        }
    }

    private static bool DrawBindingSection(string id, string label, Func<bool> drawContent)
    {
        using var idUsing = ImRaii.PushId(id);
        var changed = false;
        ImGui.Text(label);
        ImGui.Indent();
        changed |= drawContent();
        ImGui.Unindent();
        return changed;
    }

    private bool DrawBinding(KeybindDto bind)
    {
        var changed = false;

        changed |= DrawBindingSection("Key1", "Key 1:", () => DrawKey(bind.KeySettings[0]));
        changed |= DrawBindingSection("Key2", "Key 2:", () => DrawKey(bind.KeySettings[1]));
        changed |= DrawBindingSection("Game1", "Gamepad 1:", () => DrawGamepad(bind.GamepadSettings[0]));
        changed |= DrawBindingSection("Game2", "Gamepad 2:", () => DrawGamepad(bind.GamepadSettings[1]));

        return changed;
    }

    private bool DrawKey(KeyboardSettingDto setting)
    {
        var changed = false;

        if (ImGuiUtil.GenericEnumCombo("Modifier", 150f, setting.Modifier, out KeyModifierFlag mod,
                value => value.ToString().MakePretty()))
        {
            setting.Modifier = mod;
            changed = true;
        }

        ImGui.SameLine();

        if (ImGuiUtil.GenericEnumCombo("Key", 150f, setting.Key, out KeyboardVirtualKey key,
                value => value.ToString().MakePretty()))
        {
            setting.Key = key;
            changed = true;
        }

        return changed;
    }

    private bool DrawGamepad(GamepadSettingDto setting)
    {
        var changed = false;

        if (ImGuiUtil.GenericEnumCombo("Modifier", 150f, setting.Modifier, out GamepadModifierFlag mod,
                value => value.ToString().MakePretty()))
        {
            setting.Modifier = mod;
            changed = true;
        }

        ImGui.SameLine();

        if (ImGuiUtil.GenericEnumCombo("Key", 150f, setting.Key, out GamepadVirtualKey key,
                value => value.ToString().MakePretty()))
        {
            setting.Key = key;
            changed = true;
        }

        return changed;
    }
}