using Godot;
using System;

public partial class ItemSelectRes : BaseBackpackCon
{
    private BackpackPanel _owner;

    public override void Init()
    {
        base.Init();
        _owner = GetOwner() as BackpackPanel;
    }
}