using System.Collections.Generic;
using Godot;

public partial class BaseBackpackItemContainerPanel : Control, IBackpackItemContainer, IBackpackGesturePanel
{
    [Export] private Control _itemsContainer;
    protected HashSet<BackpackItem> BackpackItems;
    public BackpackPanel BackpackPanel { get; set; }

    public virtual void Init()
    {
        BackpackItems = [];

        var nodes = _itemsContainer.GetChildren();
        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is BackpackItem item)
            {
                BackpackItems.Add(item);
                item.OwnerPanel = this;
                item.Index = i;
                item.Init();
            }
        }
    }
}