using FFXIVClientStructs.FFXIV.Client.System.Input;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace Keebler;

public class KeybindProfile
{
    public int Version { get; set; } = 1;
    public string Name { get; set; } = "New Keybind Profile";
    public string? Description { get; set; }
    
    public Dictionary<InputId, Keybind> Keybinds { get; } = new();
    
    public unsafe void SetKeybinds()
    {
        foreach (var keybind in Keybinds)
        {
            try
            {
                var kb = keybind.Value;
                var kbPtr = &kb;
                UIInputData.Instance()->SetKeybind(keybind.Key, kbPtr);
            }
            catch (Exception e)
            {
                Services.Log.Error($"Failed to set keybind for {keybind.Key}: {e}");
            }
        }
    }

    public unsafe void PopulateFromGame()
    {
        Keybinds.Clear();
        var keys = Enum.GetValues<InputId>();
        foreach (var key in keys)
        {
            try
            {
                var bindPtr = UIInputData.Instance()->GetKeybind(key);
                if (bindPtr == null)
                    continue;
                Keybind bind = *bindPtr;
                Keybinds.Add(key, bind);
            }
            catch (Exception e)
            {
                Services.Log.Error($"Failed to get keybind for {key}: {e}");
            }
        }
    }
}