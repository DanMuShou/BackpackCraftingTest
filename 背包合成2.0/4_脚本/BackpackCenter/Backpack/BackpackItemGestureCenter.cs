using Godot;
using System.Collections.Generic;

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
    public EGestureType CurrentGestureMode { get; set; }

    private IBackpackGesturePanel _activePanel;

    private HashSet<IBackpackGestureItem> _affectItems;

    public void Init()
    {
        _affectItems = [];
        CurrentGestureMode = EGestureType.None;
    }

    public override void _Input(InputEvent @event)
    {
        if (!SourceItem.IsHasItem) return;

        switch (@event)
        {
            case InputEventMouseButton mouseEvent:
                SetCurrenMode(mouseEvent);
                break;
            case InputEventMouseMotion motionEvent:
                break;
        }
    }

    private void SetCurrenMode(InputEventMouseButton mouseEvent)
    {
        if (mouseEvent.Pressed)
        {
            CurrentGestureMode = mouseEvent.ButtonIndex switch
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
            CurrentGestureMode = EGestureType.None;
        }
    }

    private void MouseMoveWithPressed(InputEventMouseMotion motionEvent)
    {
    }

    public void StartGesture(IBackpackGesturePanel activePanel)
    {
        _activePanel = activePanel;
    }
}