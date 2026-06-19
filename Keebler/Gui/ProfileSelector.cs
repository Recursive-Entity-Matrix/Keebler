using Dalamud.Bindings.ImGui;
using Keebler.Models;
using Newtonsoft.Json;
using OtterGui;

namespace Keebler.Gui;

public class ProfileSelector(Config config) : ItemSelector<KeybindProfile>(config.KeybindProfiles, Flags.Add | Flags.Delete | Flags.Filter | Flags.Import)
{
    protected override bool Filtered(int idx) => Filter.Length != 0 &&
                                                 !Items[idx].Name.Contains(Filter,
                                                     StringComparison.InvariantCultureIgnoreCase);

    protected override bool OnDelete(int idx)
    {
        if (idx < 0 || idx >= config.KeybindProfiles.Count)
            return false;
        config.KeybindProfiles.RemoveAt(idx);
        config.Save();
        return true;
    }

    protected override bool OnClipboardImport(string name, string data)
    {
        var jObj = JsonConvert.DeserializeObject<KeybindProfile>(data);
        if (jObj == null)
        {
            Services.ChatGui.PrintError("Failed to import keybind profile: Invalid data");
            return false;
        }
        jObj.Name = name;
        config.KeybindProfiles.Add(jObj);
        config.Save();
        return true;
    }

    protected override bool OnAdd(string name)
    {
        var profile = new KeybindProfile
        {
            Name = name
        };
        config.KeybindProfiles.Add(profile);
        config.Save();
        return true;
    }

    protected override bool OnDraw(int idx)
    {
        return ImGui.Selectable(Items[idx].Name, CurrentIdx == idx);
    }
}