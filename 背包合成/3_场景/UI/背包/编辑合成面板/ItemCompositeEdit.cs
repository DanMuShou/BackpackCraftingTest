using Godot;
using System;

public partial class ItemCompositeEdit : HBoxContainer
{
    [Export] private SearchResourcePanel _searchRes;
    public BackpackPanel BackpackPanel { get; private set; }

    public void Init()
    {
        BackpackPanel = GetOwner<BackpackPanel>();
        _searchRes.Init();
    }
}