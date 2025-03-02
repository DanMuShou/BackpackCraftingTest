﻿using Godot;

public partial class BaseBackpackCon : Control, IBackpackCon
{
    [Export] private BackpackItem _item;
    public BackpackItemRes ItemRes { get; private set; }
    public bool LockCon { get; set; } = false;
    public bool IsCanChange { get; set; } = true;
    private int _itemCount = int.MinValue;

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

    protected virtual void RefreshItem()
    {
        // GD.Print("刷新");
        if (IsHasItem)
        {
            _item.Texture = ItemRes.BackpackIcon;
            _item.SetItem(_itemCount);
        }
        else
        {
            _item.Texture = new PlaceholderTexture2D();
            _item.SetItem(_itemCount);
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

    public void SwapRes(BaseBackpackCon other)
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
}