using Godot;

public partial class Main : Node
{
    [Export] private bool _isDebug;
    [Export] private SystemManager _systemManager;
    [Export] private BackpackPanel _backpackPanel;

    public override void _Ready()
    {
        _systemManager.Init();
        _backpackPanel.Init();

        _backpackPanel.Open = _isDebug;
    }
}