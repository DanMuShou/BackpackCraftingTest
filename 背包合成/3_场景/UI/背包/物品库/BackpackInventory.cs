using System;
using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BackpackInventory : BaseGesturePanel
{
    [Export] private GridContainer _itemsCon;
    [Export] private PackedScene _backpackItemCon;
    private BackpackPanel _owner;

    public override void Init()
    {
        _owner = GetOwner() as BackpackPanel;

        base.Init();

        var nodes = _itemsCon.GetChildren();
        for (var index = 0; index < nodes.Count; index++)
        {
            if (nodes[index] is BackpackItemCon itemCon)
            {
                _backpackGestureCons.Add(itemCon);

                itemCon.Index = index;
                itemCon.BackpackPanel = _owner;
                itemCon.Init();
            }
        }
    }

    public void AddItems(BackpackItemRes[] itemsRes)
    {
        foreach (var res in itemsRes)
        {
            foreach (var con in _backpackGestureCons)
            {
                if (con.SetRes(res))
                    break;
            }
        }
    }
}