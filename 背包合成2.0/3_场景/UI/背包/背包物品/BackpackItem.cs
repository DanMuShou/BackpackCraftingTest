using Godot;

public partial class BackpackItem : BaseBackpackItem
{
    [Export] public Button SelectButton { get; private set; }
    public int Index { get; set; }
}