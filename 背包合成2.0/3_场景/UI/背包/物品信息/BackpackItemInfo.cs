using Godot;
using System;

public partial class BackpackItemInfo : TextureRect
{
    [Export] private Label _itemCountLab;
    public void SetItemLabel(string label) => _itemCountLab.Text = label.ToString();
}