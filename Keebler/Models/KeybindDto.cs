using FFXIVClientStructs.FFXIV.Client.System.Input;

namespace Keebler.Models;

public class KeybindDto
{
    public KeyboardSettingDto[] KeySettings { get; set; } =
    [
        new(),
        new()
    ];

    public GamepadSettingDto[] GamepadSettings { get; set; } =
    [
        new(),
        new()
    ];
}

public class KeyboardSettingDto
{
    public KeyboardVirtualKey Key { get; set; }
    public KeyModifierFlag Modifier { get; set; }
}

public class GamepadSettingDto
{
    public GamepadVirtualKey Key { get; set; }
    public GamepadModifierFlag Modifier { get; set; }
}