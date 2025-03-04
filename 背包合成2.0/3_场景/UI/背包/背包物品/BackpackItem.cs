using Godot;

public partial class BackpackItem : BaseBackpackItem, IBackpackGestureItem
{
    [Export] private Button _selectButton;

    public BaseBackpackItemContainerPanel OwnerPanel { get; set; }
    public BaseBackpackItem SelectItem { get; set; }
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
    }

    private void SelectButtonOnMouseExited()
    {
    }

    private void SelectButtonOnPressed()
    {
        if (Input.IsActionJustReleased("MouseLeft"))
            HandleLeftMouseClick();
        else if (Input.IsActionJustReleased("MouseRight"))
            HandleRightMouseClick();
    }

    private void HandleLeftMouseClick()
    {
        if (!SelectItem.IsHasItem && !IsHasItem) return;

        if (SelectItem.IsHasItem && (IsHasItem && SelectItem.ItemRes == ItemRes))
        {
            SelectItem.IncreaseRes(ItemCount);
            RemoveRes();
        }
        else
        {
            SelectItem.SwapRes(this);
        }
    }

    private void HandleRightMouseClick()
    {
        if (!SelectItem.IsHasItem && IsHasItem)
        {
            SelectItem.SetRes(ItemRes, ItemCount);
        }
    }
}