using Godot;
using System.Collections.Generic;

public partial class BackpackInventory : BaseBackpackItemContainerPanel
{
    [Export] private BackpackItemRes[] text;

    private BackpackPanel _owner;

    public override void Init()
    {
        _owner = GetOwner<BackpackPanel>();
        GesturePanelType = GetType();
        base.Init();
        AddItems(text);
    }

    public void AddItems(BackpackItemRes[] itemsRes)
    {
        foreach (var res in itemsRes)
        {
            foreach (var con in BackpackItems)
            {
                if (con.SetRes(res, 100))
                    break;
            }
        }
    }
}