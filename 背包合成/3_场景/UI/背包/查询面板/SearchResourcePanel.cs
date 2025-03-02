using Godot;

public partial class SearchResourcePanel : MarginContainer
{
    [Export] private GridContainer _gridContainer;
    [Export] private PackedScene _itemConPacked;

    private ItemCompositeEdit _owner;

    public void Init()
    {
        _owner = GetOwner<ItemCompositeEdit>();

        var resourceManager = SystemManager.GetManager<ResourceManager>();
        foreach (var item in resourceManager.FoodResDic)
        {
            var itemCon = _itemConPacked.Instantiate<BackpackItemCon>();
            _gridContainer.AddChild(itemCon);

            itemCon.BackpackPanel = _owner.BackpackPanel;
            itemCon.Init();
            itemCon.SetRes(item.Value, 1000);
        }
    }
}