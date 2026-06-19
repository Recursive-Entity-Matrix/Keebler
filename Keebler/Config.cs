using Dalamud.Configuration;
using Keebler.Models;
using Newtonsoft.Json;

namespace Keebler;

public class Config : IPluginConfiguration
{
    public int Version { get; set; } = 1;
    public List<KeybindProfile> KeybindProfiles { get; set; } = new List<KeybindProfile>();
    public ConfigTypes.FeedbackType SuccessFeedbackType { get; set; } = ConfigTypes.FeedbackType.Chat;
    public ConfigTypes.FeedbackType FailureFeedbackType { get; set; } = ConfigTypes.FeedbackType.Chat;
    public ConfigTypes.MacroFeedbackType MacroFeedbackType { get; set; } = ConfigTypes.MacroFeedbackType.SuccessAndFailure;

    public void Save()
    {
        var path = Services.PluginInterface.ConfigFile.FullName;
        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public static Config Load()
    {
        var path = Services.PluginInterface.ConfigFile.FullName;
        if  (!File.Exists(path))
            return new Config();
        var json = File.ReadAllText(path);
        var jObj = JsonConvert.DeserializeObject<Config>(json);
        return jObj ?? new Config();
    }
}