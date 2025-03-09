using Godot;
using System;
using System.Collections.Generic;

public partial class ItemComposite : BaseBackpackItemContainerPanel
{
    [Export] private BackpackItem _compositeItem;
    [Export] private Label _infoLab;
    [Export] private Button _compositeBut;

    private BackpackPanel _owner;

    public override void Init()
    {
        base.Init();

        _compositeItem.Init();
    }
}