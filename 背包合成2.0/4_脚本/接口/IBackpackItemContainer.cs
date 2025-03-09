using System;
using Godot;

public interface IBackpackItemContainer
{
    public Type ContainerType { get; }

    // public BaseBackpackItem SelectItem { get; set; }
    public BackpackItemGestureCenter GestureCenter { get; set; }
    public void Init();
}