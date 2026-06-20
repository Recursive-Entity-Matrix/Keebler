using System.Numerics;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using OtterGui;

namespace Keebler.Gui;

public class ConfigWindow : Window
{
    private readonly Config _config;
    public ConfigWindow(Config config) : base("Keebler Configuration", ImGuiWindowFlags.None)
    {
        _config = config;
        Size = new Vector2(400, 200);
        SizeCondition = ImGuiCond.FirstUseEver;
    }

    public override void Draw()
    {
        var feedback = _config.FeedbackOnSuccess;
        if (ImGui.Checkbox("Print chat feedback when a profile is successfully applied", ref feedback))
        {
            _config.FeedbackOnSuccess = feedback;
            _config.Save();
        } 
    }
}