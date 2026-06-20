using Dalamud.Game.ClientState.Conditions;
using FFXIVClientStructs.FFXIV.Client.System.Input;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace Keebler.Models;

public class KeybindProfile
{
    public int Version { get; set; } = 1;
    public string Name { get; set; } = "New Keybind Profile";
    public string Description { get; set; } = string.Empty;
    
    public Dictionary<InputId, KeybindDto> Keybinds { get; } = new();
    
    public unsafe void SetKeybinds()
    {
        foreach (var keybind in Keybinds)
        {
            try
            {
                var cs = keybind.Value.ToCsKeybind();
                UIInputData.Instance()->SetKeybind(keybind.Key, &cs);
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
                Keybinds.Add(key, bind.ToKeybindDto());
            }
            catch (Exception e)
            {
                Services.Log.Error($"Failed to get keybind for {key}: {e}");
            }
        }
    }
}