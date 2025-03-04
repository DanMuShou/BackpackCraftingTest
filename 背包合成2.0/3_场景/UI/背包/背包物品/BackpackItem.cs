using Godot;

public partial class BackpackItem : BaseBackpackItem, IBackpackGestureItem
{
    [Export] private Button _selectButton;

    public BaseBackpackItemContainerPanel OwnerPanel { get; set; }
    public int Index { get; set; }

    public override void Init()
    {
        base.Init();

        _selectButton.MouseEntered += SelectButtonOnMouseEntered;
        _selectButton.MouseExited += SelectButtonOnMouseExited;
        _selectButton.Pressed += SelectButtonOnPressed;
    }


    private void SelectButtonOnMouseEntered()
    {
        GD.Print(OwnerPanel.BackpackPanel.GestureCenter.CurrentGestureMode);
    }

    private void SelectButtonOnMouseExited()
    {
    }

    private void SelectButtonOnPressed()
    {
        var selectItem = OwnerPanel.BackpackPanel.BackpackSelectItem;
        if (selectItem.IsHasItem || IsHasItem)
        {
            selectItem.SwapRes(this);
        }
    }
}