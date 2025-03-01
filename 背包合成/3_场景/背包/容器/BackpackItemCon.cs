using Godot;
using System;

public partial class BackpackItemCon : BaseBackpackCon
{
    [Export] public Button ItemSelectBut { get; private set; }
    public int Index { get; set; }
}