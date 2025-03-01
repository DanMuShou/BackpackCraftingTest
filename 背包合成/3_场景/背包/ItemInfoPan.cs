using Godot;

public partial class ItemInfoPan : PanelContainer
{
    [Export] private Label _name;
    [Export] private Label _description;
    [Export] private Label _other;

    private BackpackPanel _owner;

    public void Init()
    {
        _owner = GetOwner() as BackpackPanel;

        _name.Text = "";
        _description.Text = "";
        _other.Text = "";
        Visible = false;
    }

    public override void _Process(double delta)
    {
        if (!Visible) return;
        GlobalPosition = GetGlobalMousePosition();
    }

    public void DisplayItemInfo(bool isShow, PickedRes pickedRes)
    {
        Visible = isShow;
        if (!isShow) return;

        _name.Text = pickedRes.Name;
        _description.Text = pickedRes.Description;
    }
}