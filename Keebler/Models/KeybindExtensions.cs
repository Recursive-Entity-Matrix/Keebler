using FFXIVClientStructs.FFXIV.Client.System.Input;

namespace Keebler.Models;

public static class KeybindExtensions
{
    public static Keybind ToCsKeybind(this KeybindDto dto)
    {
        var keybind = new Keybind();
        for (int i = 0; i < 2; i++)
        {
            keybind.KeySettings[i].Key = dto.KeySettings[i].Key.ToSeVirtualKey();
            keybind.KeySettings[i].KeyModifier = dto.KeySettings[i].Modifier;
            keybind.GamepadSettings[i].Key = dto.GamepadSettings[i].Key.ToSeVirtualKey();
            keybind.GamepadSettings[i].GamepadModifier = dto.GamepadSettings[i].Modifier;
        }
        return keybind;
    }

    public static KeybindDto ToKeybindDto(this Keybind keybind)
    {
        var dto = new KeybindDto();
        for (int i = 0; i < 2; i++)
        {
            dto.KeySettings[i].Key = keybind.KeySettings[i].Key.ToKeyboardVirtualKey();
            dto.KeySettings[i].Modifier = keybind.KeySettings[i].KeyModifier;
            dto.GamepadSettings[i].Key = keybind.GamepadSettings[i].Key.ToGamepadVirtualKey();
            dto.GamepadSettings[i].Modifier = keybind.GamepadSettings[i].GamepadModifier;
        }
        return dto;
    }
}