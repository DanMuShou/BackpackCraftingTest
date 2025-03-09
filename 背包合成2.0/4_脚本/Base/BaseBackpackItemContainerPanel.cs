using System;
using System.Collections.Generic;
using Godot;

public partial class BaseBackpackItemContainerPanel : Control, IBackpackItemContainer
{
    [Export] private Control _itemsContainer;

    public Type ContainerType { get; private set; }

    // public BaseBackpackItem SelectItem { get; set; }
    public BackpackItemGestureCenter GestureCenter { get; set; }

    protected List<BackpackItem> Items;

    public virtual void Init()
    {
        ContainerType = GetType();
        Items = [];

        var nodes = _itemsContainer.GetChildren();
        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is BackpackItem item)
            {
                Items.Add(item);
                item.Index = i;

                var selectButton = item.SelectButton;
                selectButton.MouseEntered += () => HandleMouseEnterOrExit(true, item);

                item.Init();
            }
        }
    }


    // private void HandleMouseEnterOrExit(bool isMouseEntered, IBackpackItem item)
    // {
    //     if (!GestureCenter.IsGesturing || item.HasItem) return;
    //
    //     if (isMouseEntered)
    //     {
    //         if (GestureCenter._originatePanel == this)
    //         {
    //             GestureCenter.AddAffectItem(item);
    //         }
    //     }
    //     else
    //     {
    //         if (GestureCenter._originatePanel != null) return;
    //
    //         GestureCenter._originatePanel = this;
    //         GestureCenter.AddAffectItem(item);
    //     }
    // }
    private void HandleMouseEnterOrExit(bool isMouseEntered, IBackpackItem item)
    {
        if (isMouseEntered && !GestureCenter.IsGesturing)
        {
            var a = GestureCenter.TrySetOriginateItemItem(item, ContainerType);
            GD.Print($"1 : {a}");
        }
        else if (isMouseEntered && GestureCenter.IsGesturing)
        {
            var b = GestureCenter.TryAddAffectItems(item, ContainerType);
            GD.Print($"2 : {b}");
        }
    }

    // private void SelectButtonOnPressed(IBackpackItem item)
    // {
    //     if (Input.IsActionJustReleased("MouseLeft"))
    //         HandleLeftMouseClick(item);
    //     else if (Input.IsActionJustReleased("MouseRight"))
    //         HandleRightMouseClick(item);
    // }
    //
    // private void HandleLeftMouseClick(IBackpackItem item)
    // {
    //     if (!SelectItem.HasItem && !item.HasItem) return;
    //
    //     if (SelectItem.HasItem && (item.HasItem && SelectItem.ItemRes == item.ItemRes))
    //     {
    //         item.IncreaseRes(SelectItem.ItemCount);
    //         SelectItem.RemoveRes();
    //     }
    //     else
    //     {
    //         SelectItem.SwapRes(item);
    //     }
    // }
    //
    // private void HandleRightMouseClick(IBackpackItem item)
    // {
    //     if (!SelectItem.HasItem && item.HasItem)
    //     {
    //         SelectItem.SetRes(item.ItemRes, item.ItemCount / 2);
    //         item.DecreaseRes(item.ItemCount / 2);
    //     }
    //     else if (SelectItem.HasItem && item.HasItem && SelectItem.ItemRes == item.ItemRes)
    //     {
    //         item.IncreaseRes();
    //         SelectItem.DecreaseRes();
    //     }
    //     else if (SelectItem.HasItem && !item.HasItem)
    //     {
    //         item.SetRes(SelectItem.ItemRes);
    //         SelectItem.DecreaseRes();
    //     }
    // }
}