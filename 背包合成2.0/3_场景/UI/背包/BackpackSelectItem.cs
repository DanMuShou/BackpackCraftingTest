using Godot;

public partial class BackpackSelectItem : BaseBackpackItem
{
    private BackpackPanel _owner;

    public override void Init()
    {
        _owner = GetParent() as BackpackPanel;
        base.Init();

        // _owner.GestureCenter.SetSelectContainer(this);
    }

    public override void _Process(double delta)
    {
        if (!Visible) return;
        GlobalPosition = GetGlobalMousePosition();
    }

    protected override void RefreshItem()
    {
        base.RefreshItem();
        Visible = IsHasItem;
    }
}