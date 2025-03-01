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

    public void Init(BackpackSignalCenter signalCenter)
    {
        _items = [];

        var nodes = _compositeItemsCon.GetChildren();
        for (var index = 0; index < nodes.Count; index++)
        {
            if (nodes[index] is not BackpackItemCon item) continue;
            _items.Add(item);

            item.MouseEntered += () =>
            {
                if (item.IsHasItem) signalCenter.EmitIsOutputInfo(true, item.BackpackRes);
            };
            item.MouseExited += () =>
            {
                if (item.IsHasItem) signalCenter.EmitIsOutputInfo(false, item.BackpackRes);
            };

            item.Index = index;
            item.Init();
        }

        _compositeItem.Init();
    }
}