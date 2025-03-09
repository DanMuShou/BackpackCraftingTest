using System;
using System.Collections.Generic;
using Godot;

public partial class BaseBackpackItemContainerPanel : Control
{
    [Export] private Control _itemsContainer;

    public BaseBackpackItem SelectItem { get; set; }
    public BackpackItemGestureCenter GestureCenter { get; set; }

    private Type _gesturePanelType;
    protected List<BackpackItem> Items;

    public virtual void Init()
    {
        _gesturePanelType = GetType();
        Items = [];

        var nodes = _itemsContainer.GetChildren();
        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is BackpackItem item)
            {
                Items.Add(item);
                item.Index = i;
                InitGesture(item);
                item.Init();
            }
        }
    }

    private void InitGesture(BackpackItem item)
    {
        var selectButton = item.SelectButton;
        selectButton.MouseEntered += () => HandleMouseEnterOrExit(true, item);
        selectButton.MouseExited += () => HandleMouseEnterOrExit(false, item);
        selectButton.Pressed += () => SelectButtonOnPressed(item);
    }

    private void HandleMouseEnterOrExit(bool isMouseEntered, BaseBackpackItem item)
    {
        if (!GestureCenter.StartGesture || item.HasItem) return;

        if (isMouseEntered)
        {
            if (GestureCenter.ActivePanelType == _gesturePanelType)
            {
                GestureCenter.AddAffectItem(item);
            }
        }
        else
        {
            if (GestureCenter.ActivePanelType != null) return;

            GestureCenter.ActivePanelType = _gesturePanelType;
            GestureCenter.AddAffectItem(item);
        }
    }

    private void SelectButtonOnPressed(BaseBackpackItem item)
    {
        if (Input.IsActionJustReleased("MouseLeft"))
            HandleLeftMouseClick(item);
        else if (Input.IsActionJustReleased("MouseRight"))
            HandleRightMouseClick(item);
    }

    private void HandleLeftMouseClick(BaseBackpackItem item)
    {
        if (!SelectItem.HasItem && !item.HasItem) return;

        if (SelectItem.HasItem && (item.HasItem && SelectItem.ItemRes == item.ItemRes))
        {
            item.IncreaseRes(SelectItem.ItemCount);
            SelectItem.RemoveRes();
        }
        else
        {
            SelectItem.SwapRes(item);
        }
    }

    private void HandleRightMouseClick(BaseBackpackItem item)
    {
        if (!SelectItem.HasItem && item.HasItem)
        {
            SelectItem.SetRes(item.ItemRes, item.ItemCount / 2);
            item.DecreaseRes(item.ItemCount / 2);
        }
        else if (SelectItem.HasItem && item.HasItem && SelectItem.ItemRes == item.ItemRes)
        {
            item.IncreaseRes();
            SelectItem.DecreaseRes();
        }
        else if (SelectItem.HasItem && !item.HasItem)
        {
            item.SetRes(SelectItem.ItemRes);
            SelectItem.DecreaseRes();
        }
    }
}