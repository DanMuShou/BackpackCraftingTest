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
        if (!IsHasItem && !BackpackPanel.ItemSelectCon.IsHasItem) return;
        var itemSelectCon = BackpackPanel.ItemSelectCon;

        if (Input.IsKeyPressed(Key.Shift))
        {
            switch (IsHasItem)
            {
                case true when !itemSelectCon.IsHasItem:
                    itemSelectCon.SetRes(ItemRes);
                    DecreaseRes();
                    return;
                case false when itemSelectCon.IsHasItem:
                    SetRes(itemSelectCon.ItemRes);
                    itemSelectCon.DecreaseRes();
                    return;
                case true when itemSelectCon.IsHasItem && ItemRes == itemSelectCon.ItemRes:
                    DecreaseRes();
                    itemSelectCon.IncreaseRes();
                    return;
            }
        }
        else
        {
            if (ItemRes == itemSelectCon.ItemRes)
            {
                IncreaseRes(itemSelectCon.ItemCount);
                itemSelectCon.RemoveRes();
                return;
            }

            SwapRes(itemSelectCon);
        }
    }
}