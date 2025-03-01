using Godot;
using System.Collections.Generic;

public partial class BackpackInventory : VBoxContainer
{
    [Export] private GridContainer _itemsCon;
    [Export] private PackedScene _backpackItemCon;

    private int _containerCount = 30;
    private List<BackpackItemCon> _itemCons;

    public void Init(BackpackSignalCenter signalCenter)
    {
        _itemCons = [];

        for (var i = 0; i < _containerCount; i++)
        {
            var item = _backpackItemCon.Instantiate<BackpackItemCon>();

            _itemCons.Add(item);
            _itemsCon.AddChild(item);

            item.MouseEntered += () =>
            {
                if (item.IsHasItem) signalCenter.EmitIsOutputInfo(true, item.BackpackRes);
            };
            item.MouseExited += () =>
            {
                if (item.IsHasItem) signalCenter.EmitIsOutputInfo(false, item.BackpackRes);
            };

            item.Index = i;
            item.Init();
        }
    }

    public void AddItem(PickedRes pickedRes)
    {
        foreach (var itemCon in _itemCons)
        {
            if (itemCon.AddItem(pickedRes))
            {
                break;
            }
        }
    }
}