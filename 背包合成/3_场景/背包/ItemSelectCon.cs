using Godot;

public partial class ItemSelectCon : BaseBackpackCon
{
    private BackpackPanel _owner;

    public override void Init()
    {
        _owner = GetParent() as BackpackPanel;
        base.Init();
    }

    public override void _Process(double delta)
    {
        if (!Visible) return;
        GlobalPosition = _owner.GetGlobalMousePosition();
    }

    protected override void RefreshItem()
    {
        base.RefreshItem();

        if (IsHasItem)
            _owner.SignalCenter.EmitIsShowInfo(false, null);

        Visible = IsHasItem;
    }
}