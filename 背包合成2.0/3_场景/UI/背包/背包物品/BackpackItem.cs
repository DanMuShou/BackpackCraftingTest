using Godot;

public partial class BackpackItem : BaseBackpackItem, IBackpackGesture
{
    [Export] public Button SelectButton { get; private set; }
    public int Index { get; set; }
}