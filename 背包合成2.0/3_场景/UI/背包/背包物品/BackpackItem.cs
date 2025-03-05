using Godot;

public partial class BackpackItem : BaseBackpackItem, IBackpackGestureItem
{
    [Export] private Button _selectButton;

    public BaseBackpackItemContainerPanel OwnerPanel { get; set; }
    public BaseBackpackItem SelectItem { get; set; }
    public BackpackItemGestureCenter GestureCenter { get; set; }
    public int Index { get; set; }

    public override void Init()
    {
        base.Init();

        _selectButton.MouseEntered += () => HandleMouseEnterOrExit(true);
        _selectButton.MouseExited += () => HandleMouseEnterOrExit(false);
        _selectButton.Pressed += SelectButtonOnPressed;
    }


    private void HandleMouseEnterOrExit(bool isMouseEntered)
    {
        if (!SelectItem.HasItem || !GestureCenter.IsMouseLeftOrRightPress || HasItem) return;

        if (isMouseEntered)
        {
            if (GestureCenter.ActivePanelType == OwnerPanel.GesturePanelType)
            {
                GestureCenter.AddAffectItem(this);
            }
        }
        else
        {
            if (GestureCenter.ActivePanelType == null)
            {
                GestureCenter.ActivePanelType = OwnerPanel.GesturePanelType;
                GestureCenter.AddAffectItem(this);
            }
        }
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
        if (!SelectItem.HasItem && !HasItem) return;

        if (SelectItem.HasItem && (HasItem && SelectItem.ItemRes == ItemRes))
        {
            IncreaseRes(SelectItem.ItemCount);
            SelectItem.RemoveRes();
        }
        else
        {
            SelectItem.SwapRes(this);
        }
    }

    private void HandleRightMouseClick()
    {
        if (!SelectItem.HasItem && HasItem)
        {
            SelectItem.SetRes(ItemRes, ItemCount / 2);
            DecreaseRes(ItemCount / 2);
        }
        else if (SelectItem.HasItem && HasItem && SelectItem.ItemRes == ItemRes)
        {
            IncreaseRes();
            SelectItem.DecreaseRes();
        }
        else if (SelectItem.HasItem && !HasItem)
        {
            SetRes(SelectItem.ItemRes);
            SelectItem.DecreaseRes();
        }
    }
}