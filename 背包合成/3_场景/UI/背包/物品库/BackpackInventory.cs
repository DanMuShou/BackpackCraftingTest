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
            foreach (var con in _itemCons)
            {
                if (con.SetRes(res))
                    break;
            }
        }
    }
}