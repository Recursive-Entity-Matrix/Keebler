using Dalamud.Game.ClientState.Keys;
using FFXIVClientStructs.FFXIV.Client.System.Input;

namespace Keebler.Models;

public static class VirtualKeyExtensions
{
    public static SeVirtualKey ToSeVirtualKey(this KeyboardVirtualKey key) => (SeVirtualKey)(byte)key;

    public static SeVirtualKey ToSeVirtualKey(this GamepadVirtualKey key) => (SeVirtualKey)(byte)key;

    public static KeyboardVirtualKey ToKeyboardVirtualKey(this SeVirtualKey key) => (KeyboardVirtualKey)key;

    public static GamepadVirtualKey ToGamepadVirtualKey(this SeVirtualKey key) => (GamepadVirtualKey)key;
}