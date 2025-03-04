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

    public void Init()
    {
        SignalCenter.OnIsShowInfo += (isShow, res) => _itemInfoPan.DisplayItemInfo(isShow, res);

        GestureCenter.BackpackPanel = this;
        GestureCenter.SourceItem = BackpackSelectItem;
        GestureCenter.Init();

        _backpackInventory.BackpackPanel = this;
        _backpackInventory.Init();

        _recipeCompositePanel.Init();
        _itemRecipeSearchPanel.Init();
        _itemComposite.Init();
        _itemInfoPan.Init();
        BackpackSelectItem.Init();
    }
}