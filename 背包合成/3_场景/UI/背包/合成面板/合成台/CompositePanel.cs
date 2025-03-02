using System.Collections.Generic;
using Godot;

public partial class CompositePanel : VBoxContainer
{
    [Export] private GridContainer _gridContainer;
    [Export] private BackpackItemCon _outputCon;
    [Export] private Button _compositeBut;
    [Export] private Label _logLab;

    private BackpackItemCon[] _itemCons;

    private ItemCompositeEdit _owner;

    public void Init()
    {
        _owner = GetOwner<ItemCompositeEdit>();

        _itemCons = new BackpackItemCon[9];

        foreach (var node in _gridContainer.GetChildren())
        {
            if (node is not BackpackItemCon con) continue;

            con.BackpackPanel = _owner.BackpackPanel;
            con.Index = _itemCons.Length;

            con.Init();

            _itemCons[^1] = con;
        }

        _outputCon.BackpackPanel = _owner.BackpackPanel;
        _outputCon.Init();
    }
}