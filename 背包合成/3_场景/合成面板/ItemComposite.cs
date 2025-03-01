using Godot;
using System;
using System.Collections.Generic;

public partial class ItemComposite : CenterContainer
{
    [Export] private GridContainer _compositeItemsCon;

    [Export] private BackpackItemCon _compositeItem;
    [Export] private Label _infoLab;
    [Export] private Button _compositeBut;

    private List<BackpackItemCon> _items;

    private BackpackPanel _owner;

    public void Init()
    {
        _items = [];
        _owner = GetOwner() as BackpackPanel;

        var nodes = _compositeItemsCon.GetChildren();
        for (var index = 0; index < nodes.Count; index++)
        {
            if (nodes[index] is not BackpackItemCon itemCon) continue;
            _items.Add(itemCon);
            itemCon.Index = index;
            itemCon.Init();

            itemCon.ItemSelectBut.MouseEntered += () =>
            {
                if (itemCon.IsHasItem) _owner.SignalCenter.EmitIsShowInfo(true, itemCon.ItemRes);
            };
            itemCon.ItemSelectBut.MouseExited += () =>
            {
                if (itemCon.IsHasItem) _owner.SignalCenter.EmitIsShowInfo(false, itemCon.ItemRes);
            };
        }

        _compositeItem.Init();
    }
}