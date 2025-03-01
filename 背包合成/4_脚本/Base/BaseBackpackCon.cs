using Godot;

public partial class BaseBackpackCon : Node, IBackpackCon
{
    [Export] private BackpackItem _item;
    public PickedRes ItemRes { get; private set; }
    private int _itemCount;

    public int ItemCount
    {
        get => _itemCount;
        set
        {
            _itemCount = value;
            RefreshItem();
        }
    }

    public bool IsHasItem => _itemCount > 0;

    public virtual void Init()
    {
        ItemCount = 0;
    }

    private void RefreshItem()
    {
        if (IsHasItem)
        {
            _item.Visible = true;
            _item.Texture = ItemRes.BackpackIcon;
            _item.SetItem(_itemCount);
        }
        else
        {
            _item.Visible = false;
            _item.Texture = null;
            _item.SetItem(_itemCount);
            ItemRes = null;
        }
    }

    public bool AddItemRes(PickedRes itemRes)
    {
        if (!IsHasItem)
        {
            ItemRes = itemRes;
            ItemCount++;
            return true;
        }
        else if (itemRes.UniqueId == ItemRes.UniqueId)
        {
            ItemCount++;
            return true;
        }

        return false;
    }

    public PickedRes OutputItemRes()
    {
        if (!IsHasItem) return null;
        var itemRes = ItemRes;
        ItemCount -= 1;
        return itemRes;
    }
}