using Godot;
using System;
using System.Collections.Generic;

public partial class ItemRecipeSearchPanel : VBoxContainer
{
    [Export] private Button _searchBut;
    [Export] private CheckButton _modeBut;
    [Export] private LineEdit _selectNameEdit;
    [Export] private TabContainer _tabContainer;
    [Export] private GridContainer _resourceContainer;
    [Export] private GridContainer _recipeContainer;
    [Export] private PackedScene _itemPacked;


    private List<BackpackItem> _itemConList;
    private string _selectName;

    public void Init()
    {
        _itemConList = [];

        var resourceManager = SystemManager.GetManager<ResourceManager>();

        _selectNameEdit.TextChanged += OnSelectNameChange;
        _modeBut.Pressed += () => _tabContainer.CurrentTab = !_modeBut.IsPressed() ? 0 : 1;

        foreach (var res in resourceManager.BackpackResDic[EItemType.Food])
        {
            var itemCon = _itemPacked.Instantiate<BackpackItem>();
            _resourceContainer.AddChild(itemCon);

            _itemConList.Add(itemCon);

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

        foreach (var itemCon in _itemConList)
        {
            itemCon.Visible = itemCon.ItemRes.Name.Contains(text);
        }
    }
}