using Godot;

public partial class BackpackPanel : Control
{
    [Export] private int _count = 4;

    [Export] public BackpackSignalCenter SignalCenter { get; private set; }
    [Export] public BackpackItemGestureCenter GestureCenter { get; private set; }
    [Export] public BackpackSelectItem BackpackSelectItem { get; private set; }

    [Export] private BackpackInventory _backpackInventory;
    [Export] private RecipeCompositePanel _recipeCompositePanel;
    [Export] private ItemRecipeSearchPanel _itemRecipeSearchPanel;
    [Export] private ItemComposite _itemComposite;
    [Export] private ItemInfoPan _itemInfoPan;

    public bool Open
    {
        get => Open;
        set
        {
            ProcessMode = value ? ProcessModeEnum.Inherit : ProcessModeEnum.Disabled;
            Visible = value;
        }
    }


    public void Init()
    {
        GestureCenter.SelectItem = BackpackSelectItem;
        GestureCenter.Init();

        _backpackInventory.GestureCenter = GestureCenter;
        // _backpackInventory.SelectItem = BackpackSelectItem;
        _backpackInventory.Init();

        _recipeCompositePanel.GestureCenter = GestureCenter;
        // _recipeCompositePanel.SelectItem = BackpackSelectItem;
        _recipeCompositePanel.Init();

        _itemComposite.GestureCenter = GestureCenter;
        // _itemComposite.SelectItem = BackpackSelectItem;
        _itemComposite.Init();

        _itemRecipeSearchPanel.Init();
        _itemInfoPan.Init();
        BackpackSelectItem.Init();
    }
}