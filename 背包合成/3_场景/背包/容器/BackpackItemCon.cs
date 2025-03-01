using Godot;
using System;

public partial class BackpackItemCon : Button
{
    [Export] private TextureRect _itemIcon;
    [Export] private Label _itemCountLab;

    public PickedRes BackpackRes { get; private set; }
    public int Index { get; set; }
    public bool IsHasItem => _itemCount > 0;


    private int ItemCount
    {
        get => _itemCount;
        set
        {
            _itemCount = value;
            Refresh();
        }
    }

    private int _itemCount;

    public void Init()
    {
        ItemCount = 0;
    }

    private void Refresh()
    {
        if (IsHasItem)
        {
            _itemIcon.Visible = true;
            _itemIcon.Texture = BackpackRes.BackpackIcon;
            _itemCountLab.Text = _itemCount > 1 ? $"{_itemCount.ToString()} " : "";
        }
        else
        {
            _itemIcon.Visible = false;
            _itemIcon.Texture = null;
            _itemCountLab.Text = "";
            BackpackRes = null;
        }
    }

    public bool AddItem(PickedRes backpackRes)
    {
        if (!IsHasItem)
        {
            BackpackRes = backpackRes;
            ItemCount += 1;
            return true;
        }
        else if (BackpackRes.UniqueId == backpackRes.UniqueId)
        {
            ItemCount += 1;
            return true;
        }

        GD.Print("添加物品失败");
        return false;
    }

    public PickedRes OutputItemRes()
    {
        if (!IsHasItem) return null;
        ItemCount -= 1;
        return BackpackRes;
    }
}