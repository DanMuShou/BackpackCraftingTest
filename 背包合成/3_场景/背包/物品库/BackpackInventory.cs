using Godot;
using System.Collections.Generic;

public partial class BackpackInventory : VBoxContainer
{
    [Export] private GridContainer _itemsCon;
    [Export] private PackedScene _backpackItemCon;

    private List<BackpackItemCon> _itemCons;

    private BackpackPanel _owner;

    public void Init()
    {
        _itemCons = [];
        _owner = GetOwner() as BackpackPanel;

        var nodes = _itemsCon.GetChildren();
        for (var index = 0; index < nodes.Count; index++)
        {
            if (nodes[index] is BackpackItemCon itemCon)
            {
                _itemCons.Add(itemCon);

                itemCon.ItemSelectBut.MouseEntered += () =>
                {
                    if (itemCon.IsHasItem) _owner.SignalCenter.EmitIsShowInfo(true, itemCon.ItemRes);
                };
                itemCon.ItemSelectBut.MouseExited += () =>
                {
                    if (itemCon.IsHasItem) _owner.SignalCenter.EmitIsShowInfo(false, itemCon.ItemRes);
                };

                itemCon.Index = index;
                itemCon.Init();
            }
        }
    }

    public void AddItem(PickedRes pickedRes)
    {
        foreach (var itemCon in _itemCons)
        {
            if (itemCon.AddItemRes(pickedRes))
            {
                break;
            }
        }
    }
}