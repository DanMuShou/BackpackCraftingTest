using System;
using Godot;
using System.Collections.Generic;
using System.Linq;

public enum EGestureType
{
    None,
    Left,
    LeftShift,
    Right,
    RightShift,
}

public partial class BackpackItemGestureCenter : Node
{
    public BackpackPanel BackpackPanel { get; set; }
    public BaseBackpackItem SourceItem { get; set; }
    public EGestureType DragGestureMode { get; private set; }

    public Type ActivePanelType { get; set; }
    public bool IsMouseLeftOrRightPress => DragGestureMode != EGestureType.None;
    private HashSet<BaseBackpackItem> _affectItems;

    public void Init()
    {
        _affectItems = [];
        DragGestureMode = EGestureType.None;
    }

    public override void _Input(InputEvent @event)
    {
        if (!SourceItem.HasItem) return;

        if (@event is InputEventMouseButton mouseEvent)
            SetCurrenMode(mouseEvent);
    }

    private void SetCurrenMode(InputEventMouseButton mouseEvent)
    {
        if (mouseEvent.Pressed)
        {
            DragGestureMode = mouseEvent.ButtonIndex switch
            {
                MouseButton.Left when mouseEvent.ShiftPressed => EGestureType.LeftShift,
                MouseButton.Right when mouseEvent.ShiftPressed => EGestureType.RightShift,
                MouseButton.Left => EGestureType.Left,
                MouseButton.Right => EGestureType.Right,
                _ => EGestureType.None
            };
        }
        else if (mouseEvent.IsReleased())
        {
            ResetMode();
        }
    }

    private void UpdateAffectItem()
    {
        if (ActivePanelType == null) return;
        var averageCount = SourceItem.ItemCount / _affectItems.Count;
        foreach (var item in _affectItems)
        {
            if (!item.HasItem)
            {
                item.SetRes(SourceItem.ItemRes, averageCount);
            }
            else
            {
                item.SetCount(averageCount);
            }
        }
    }

    private void A()
    {
        if (ActivePanelType == null) return;
        var averageCount = SourceItem.ItemCount / _affectItems.Count;
        SourceItem.DecreaseRes(averageCount * _affectItems.Count);
    }

    private void ResetMode()
    {
        DragGestureMode = EGestureType.None;
        ActivePanelType = null;
        _affectItems.Clear();
    }


    public void AddAffectItem(BaseBackpackItem affectItem)
    {
        if (_affectItems.Add(affectItem))
        {
            UpdateAffectItem();
        }
    }
}