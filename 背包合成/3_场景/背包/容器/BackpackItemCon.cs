using Godot;
using System;

public partial class BackpackItemCon : BaseBackpackCon
{
    [Export] public Button ItemSelectBut { get; private set; }
    public int Index { get; set; }

    public BackpackPanel BackpackPanel { get; set; }

    public override void Init()
    {
        base.Init();
        ItemSelectBut.MouseEntered += OnButMouseEntered;
        ItemSelectBut.MouseExited += OnButMouseExited;
        ItemSelectBut.Pressed += OnButMousePressed;
    }

    private void OnButMouseEntered()
    {
        if (IsHasItem && !BackpackPanel.ItemSelectCon.IsHasItem)
        {
            BackpackPanel.SignalCenter.EmitIsShowInfo(true, ItemRes);
        }
    }

    private void OnButMouseExited()
    {
        if (IsHasItem && !BackpackPanel.ItemSelectCon.IsHasItem)
        {
            BackpackPanel.SignalCenter.EmitIsShowInfo(false, null);
        }
    }

    private void OnButMousePressed()
    {
        if (IsHasItem || BackpackPanel.ItemSelectCon.IsHasItem)
        {
            if (ItemRes == BackpackPanel.ItemSelectCon.ItemRes)
            {
                IncreaseRes(BackpackPanel.ItemSelectCon.ItemCount);
                BackpackPanel.ItemSelectCon.RemoveRes();
                return;
            }

            SwapRes(BackpackPanel.ItemSelectCon);
        }
    }
}