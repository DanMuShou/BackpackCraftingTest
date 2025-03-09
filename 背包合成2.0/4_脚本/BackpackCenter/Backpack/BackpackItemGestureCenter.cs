using System;
using Godot;
using System.Collections.Generic;
using System.Linq;


public partial class BackpackItemGestureCenter : Node
{
    public bool StartGesture => (DragGestureMode != EGestureType.None) && _startSelectCount != 0;
    public BaseBackpackItem SelectItem { get; set; }
    public Type ActivePanelType { get; set; }

    private enum EGestureType
    {
        None,
        Left,
        LeftShift,
        Right,
        RightShift,
    }

    private EGestureType DragGestureMode { get; set; }
    private HashSet<BaseBackpackItem> _affectItems;

    private int _startSelectCount;
    private BackpackItemRes _startResource;

    public void Init()
    {
        _affectItems = [];
        DragGestureMode = EGestureType.None;
        _startSelectCount = 0;
        _startResource = null;
    }

    public override void _Input(InputEvent @event)
    {
        GD.Print(1);
        if (@event is InputEventMouseButton mouseEvent)
            SetCurrenMode(mouseEvent);
    }

    private void SetCurrenMode(InputEventMouseButton mouseEvent)
    {
        if (mouseEvent.Pressed)
        {
            if (StartGesture) return;

            DragGestureMode = mouseEvent.ButtonIndex switch
            {
                MouseButton.Left when mouseEvent.ShiftPressed => EGestureType.LeftShift,
                MouseButton.Right when mouseEvent.ShiftPressed => EGestureType.RightShift,
                MouseButton.Left => EGestureType.Left,
                MouseButton.Right => EGestureType.Right,
                _ => EGestureType.None
            };

            if (DragGestureMode != EGestureType.None && SelectItem.HasItem)
            {
                _startSelectCount = SelectItem.ItemCount;
                _startResource = SelectItem.ItemRes;
            }
        }
        else if (mouseEvent.IsReleased())
        {
            ResetMode();
        }
    }

    private void UpdateAffectItem()
    {
        if (_affectItems.Count == 0 || _startSelectCount == 0) return;
        switch (DragGestureMode)
        {
            case EGestureType.Left:
                GestureLeft();
                break;
            case EGestureType.Right:
                GestureRight();
                break;
        }
    }

    private void GestureLeft()
    {
        var averageCount = _startSelectCount / _affectItems.Count;
        var remainderCount = _startSelectCount % _affectItems.Count;

        foreach (var item in _affectItems)
        {
            if (averageCount > 0)
            {
                SetGestureItem(item, averageCount);
                continue;
            }

            if (remainderCount > 0)
            {
                SetGestureItem(item, 1);
                remainderCount--;
            }
            else if (remainderCount == 0)
            {
                break;
            }
        }

        if (remainderCount > 0)
            SetGestureItem(SelectItem, remainderCount);
        else
            SelectItem.RemoveRes();
    }

    private void GestureRight()
    {
        var remainderCount = _startSelectCount;

        foreach (var item in _affectItems)
        {
            if (remainderCount > 0)
            {
                SetGestureItem(item, 1);
                remainderCount -= 1;
            }
            else
            {
                break;
            }
        }

        if (remainderCount > 0)
            SetGestureItem(SelectItem, remainderCount);
        else
            SelectItem.RemoveRes();
    }

    private void SetGestureItem(BaseBackpackItem item, int count = 1)
    {
        if (item.HasItem)
            item.SetCount(count);
        else
            item.SetRes(_startResource, count);
    }

    private void ResetMode()
    {
        DragGestureMode = EGestureType.None;
        _startSelectCount = 0;
        _startResource = null;
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