using Godot;
using System;

public partial class Main : Node
{
    [Export] private SystemManager _systemManager;
    [Export] private BackpackPanel _backpackPanel;

    public override void _Ready()
    {
        _systemManager.Init();
        _backpackPanel.Init();
    }
}