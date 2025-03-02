using System.Collections.Generic;
using Godot;

public partial class SearchResourcePanel : MarginContainer
{
    [Export] private CheckButton _modeBut;
    [Export] private LineEdit _selectNameEdit;
    [Export] private TabContainer _tabContainer;
    [Export] private GridContainer _resCon;
    [Export] private GridContainer _recipeCon;
    [Export] private PackedScene _itemConPacked;

    private ItemCompositeEdit _owner;

    private List<BackpackItemCon> _itemConList;
    private string _selectName;

    public void Init()
    {
        _owner = GetOwner<ItemCompositeEdit>();
        _itemConList = [];

        var resourceManager = SystemManager.GetManager<ResourceManager>();

        _selectNameEdit.TextChanged += OnSelectNameChange;
        _modeBut.Pressed += () => _tabContainer.CurrentTab = !_modeBut.IsPressed() ? 0 : 1;

        foreach (var res in resourceManager.BackpackResDic[EItemType.Food])
        {
            var itemCon = _itemConPacked.Instantiate<BackpackItemCon>();
            _resCon.AddChild(itemCon);

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

        foreach (var itemCon in _itemConList)
        {
            itemCon.Visible = itemCon.ItemRes.Name.Contains(text);
        }
    }
}