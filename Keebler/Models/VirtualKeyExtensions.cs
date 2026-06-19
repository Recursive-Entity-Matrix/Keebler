using Dalamud.Game.ClientState.Keys;
using FFXIVClientStructs.FFXIV.Client.System.Input;

namespace Keebler.Models;

public static class VirtualKeyExtensions
{
    private const byte KeyboardMin = (byte)KeyboardVirtualKey.LBUTTON;   // 1
    private const byte KeyboardMax = (byte)KeyboardVirtualKey.F24;      // 135
    private const byte GamepadMin  = (byte)GamepadVirtualKey.PAD_LMB;   // 160
    private const byte GamepadMax  = (byte)GamepadVirtualKey.PAD_Start; // 182
    
    public static SeVirtualKey ToSeVirtualKey(this KeyboardVirtualKey key) => (SeVirtualKey)(byte)key;
    
    public static SeVirtualKey ToSeVirtualKey(this GamepadVirtualKey key) => (SeVirtualKey)(byte)key;
    
    public static KeyboardVirtualKey ToKeyboardVirtualKey(this SeVirtualKey key) => (KeyboardVirtualKey)key;
    
    public static GamepadVirtualKey ToGamepadVirtualKey(this SeVirtualKey key) => (GamepadVirtualKey)key;

    public static VirtualKeyType GetVirtualKeyType(this SeVirtualKey key)
    {
        var byteValue = (byte)key;
        if (byteValue is >= KeyboardMin and <= KeyboardMax)
            return VirtualKeyType.Keyboard;
        if (byteValue is >= GamepadMin and <= GamepadMax)
            return VirtualKeyType.Gamepad;
        return VirtualKeyType.Unknown;
    }
}