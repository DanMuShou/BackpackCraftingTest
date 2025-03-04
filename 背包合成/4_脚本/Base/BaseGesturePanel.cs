using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class BaseGesturePanel : Control, IBackpackGesturePanel
{
    protected HashSet<BackpackItemCon> _backpackGestureCons;
    private Rect2 _panelRect;

    public virtual void Init()
    {
        _backpackGestureCons = [];
        _panelRect = new Rect2(GlobalPosition, Size);
    }

    public bool IsInPanel(Vector2 point)
        => _panelRect.HasPoint(point);

    public BackpackItemCon FindConAtPoint(Vector2 point)
        => _backpackGestureCons.FirstOrDefault(con => con.ContainsPoint(point));
}