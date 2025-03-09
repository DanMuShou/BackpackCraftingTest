using Godot;
using System.Collections.Generic;

public partial class BackpackInventory : BaseBackpackItemContainerPanel
{
    [Export] private BackpackItemRes[] text;

    private BackpackPanel _owner;

    public override void Init()
    {
        _owner = GetOwner<BackpackPanel>();
        base.Init();
        AddItems(text);
    }

    private void AddItems(BackpackItemRes[] itemsRes)
    {
        foreach (var res in itemsRes)
        {
            foreach (var con in Items)
            {
                if (con.SetRes(res, 5))
                    break;
            }
        }
    }
}