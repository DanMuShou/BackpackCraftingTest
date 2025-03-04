using System;
using System.Collections.Generic;
using Godot;

public interface IBackpackGesturePanel
{
    public bool IsInPanel(Vector2 point);
    public BackpackItemCon FindConAtPoint(Vector2 point);
}