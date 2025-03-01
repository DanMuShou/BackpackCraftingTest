using Godot;
using System;

public partial class BackpackItem : TextureRect
{
    [Export] private Label _itemCountLab;

    public void SetItem(int itemCount)
    {
        _itemCountLab.Text = itemCount > 0 ? itemCount.ToString() : "";
    }
}