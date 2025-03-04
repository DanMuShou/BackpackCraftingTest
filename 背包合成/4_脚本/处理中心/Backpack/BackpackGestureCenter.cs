using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class BackpackGestureCenter : Node
{
    private enum GestureMode
    {
        None,
        Left,
        LeftShift,
        Right,
        RightShift
    }

    private GestureMode _curMode = GestureMode.None;
    private Vector2 _strPos;

    private HashSet<IBackpackGesturePanel> _gesturePanels;

    private IBackpackGesturePanel _sourceGesturePanel;
    private BaseBackpackCon _sourceCon;
    private HashSet<BackpackItemCon> _affectedCons;

    private void Init()
    {
        _affectedCons = [];
        _gesturePanels = [];
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        switch (@event)
        {
            case InputEventMouseButton mouseButEvent:
                HandleMouseButton(mouseButEvent);
                break;
            case InputEventMouseMotion mouseMotEvent:
                // HandleMouseMotion(mouseMotEvent);
                break;
        }
    }

    private void HandleMouseButton(InputEventMouseButton mouseButEvent)
    {
        if (mouseButEvent.ButtonIndex is MouseButton.Left or MouseButton.Right && mouseButEvent.Pressed)
        {
            _curMode = mouseButEvent.ButtonIndex switch
            {
                MouseButton.Left when mouseButEvent.ShiftPressed => GestureMode.LeftShift,
                MouseButton.Left => GestureMode.Left,
                MouseButton.Right when mouseButEvent.ShiftPressed => GestureMode.RightShift,
                MouseButton.Right => GestureMode.Right,
                _ => GestureMode.None
            };

            if (_sourceGesturePanel == null)
            {
                _sourceGesturePanel =
                    _gesturePanels.FirstOrDefault(panel => panel.IsInPanel(mouseButEvent.Position));
            }
        }
        else if (mouseButEvent.IsReleased())
        {
            if (_curMode != GestureMode.None && _sourceCon != null)
            {
                ExecuteDistribution();
                _curMode = GestureMode.None;
                _sourceGesturePanel = null;
                _sourceCon = null;
                _affectedCons.Clear();
            }
        }
    }

    private void HandleMouseMotion(InputEventMouseMotion mouseMotEvent)
    {
        if (_curMode == GestureMode.None || _sourceCon == null) return;

        var motionCon = _sourceGesturePanel.FindConAtPoint(mouseMotEvent.Position);

        if (motionCon is { IsHasItem: false })
            _affectedCons.Add(motionCon);
    }

    private void ExecuteDistribution()
    {
        if (_sourceCon.ItemCount <= 0) return;

        switch (_curMode)
        {
            case GestureMode.Left:
                break;
            case GestureMode.LeftShift:
                break;
            case GestureMode.Right:
                break;
            case GestureMode.RightShift:
                break;
        }
    }

    public void SetSourceCon(BaseBackpackCon sourceCon)
        => _sourceCon = sourceCon.IsHasItem ? sourceCon : null;

    public void AddGesturePanel(IBackpackGesturePanel gesturePanel)
        => _gesturePanels.Add(gesturePanel);
}