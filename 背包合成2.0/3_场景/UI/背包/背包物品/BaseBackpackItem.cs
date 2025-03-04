using Godot;

public partial class BaseBackpackItem : Control, IBackpackContainer
{
    [Export] private BackpackItemInfo _itemInfo;
    public BackpackItemRes ItemRes { get; private set; }
    public bool LockCon { get; set; } = false;
    public bool IsCanChange { get; set; } = true;
    private int _itemCount = int.MinValue;
    private Rect2 _rect;

    public int ItemCount
    {
        get => _itemCount;
        private set
        {
            if (_itemCount == value) return;
            _itemCount = value;
            RefreshItem();
        }
    }

    public bool IsHasItem => _itemCount > 0;

    public virtual void Init()
    {
        ItemCount = 0;
    }

    public override void _Process(double delta)
    {
        _rect = new Rect2(GlobalPosition, Size);
        SetProcess(false);
    }

    protected virtual void RefreshItem()
    {
        if (IsHasItem)
        {
            _itemInfo.Texture = ItemRes.BackpackIcon;
            _itemInfo.SetItemLabel(_itemCount == 0 ? "" : ItemCount.ToString());
        }
        else
        {
            _itemInfo.Texture = new PlaceholderTexture2D();
            _itemInfo.SetItemLabel(_itemCount == 0 ? "" : ItemCount.ToString());
            ItemRes = null;
        }
        // _item.Visible = IsHasItem;
    }

    public bool SetRes(BackpackItemRes itemRes, int count = 1)
    {
        if (IsCanChange)
        {
            if (IsHasItem) return false;
            ItemRes = itemRes;
            ItemCount = count;
        }
        else
        {
            if (IsHasItem) return false;
            ItemRes = itemRes;
            ItemCount = 1;
        }

        return true;
    }

    public void RemoveRes()
    {
        if (!IsCanChange) return;
        if (!IsHasItem) return;
        ItemCount = 0;
    }

    public void DecreaseRes(int removeCount = 1)
    {
        if (!IsCanChange) return;
        if (!IsHasItem) return;

        if (removeCount >= ItemCount)
            ItemCount = 0;
        else
            ItemCount -= removeCount;
    }

    public void IncreaseRes(int addCount = 1)
    {
        if (!IsCanChange) return;
        if (!IsHasItem) return;

        ItemCount += addCount;
    }

    public void SwapRes(BaseBackpackItem other)
    {
        if (IsCanChange)
        {
            if (other == null) return;
            var otherRes = other.ItemRes;
            var otherCount = other.ItemCount;

            other.RemoveRes();
            other.SetRes(ItemRes, ItemCount);

            RemoveRes();
            SetRes(otherRes, otherCount);
        }
        else
        {
            if (other == null) return;
            other.RemoveRes();
            other.SetRes(ItemRes);
        }
    }

    public bool ContainsPoint(Vector2 point) => _rect.HasPoint(point);
}