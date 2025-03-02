using System.Collections.Generic;
using Godot;

public partial class SearchResourcePanel : MarginContainer
{
    [Export] private LineEdit _itemNameEdit;
    [Export] private GridContainer _gridContainer;
    [Export] private PackedScene _itemConPacked;

    private ItemCompositeEdit _owner;

    private List<BackpackItemCon> _itemConList;
    private string _selectName;

    public void Init()
    {
        _owner = GetOwner<ItemCompositeEdit>();
        _itemConList = [];

        _itemNameEdit.TextChanged += OnSelectNameChange;

        var resourceManager = SystemManager.GetManager<ResourceManager>();
        foreach (var res in resourceManager.BackpackResDic[EItemType.Food])
        {
            var itemCon = _itemConPacked.Instantiate<BackpackItemCon>();
            _gridContainer.AddChild(itemCon);

            _itemConList.Add(itemCon);

            itemCon.BackpackPanel = _owner.BackpackPanel;
            itemCon.Init();
            itemCon.IsCanChange = false;
            itemCon.SetRes(res);
        }
    }

    private void OnSelectNameChange(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            foreach (var con in _itemConList)
                con.Visible = true;

            return;
        }

        GD.Print(text);
        foreach (var itemCon in _itemConList)
        {
            itemCon.Visible = itemCon.ItemRes.Name.Contains(text);
        }
    }
}