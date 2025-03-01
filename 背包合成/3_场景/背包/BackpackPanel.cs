using Godot;

public partial class BackpackPanel : Control
{
    [Export] private int _count = 2;
    [Export] private PickedRes[] _pickedRes;

    [Export] public BackpackSignalCenter SignalCenter { get; private set; }

    [Export] private BackpackInventory _backpackInventory;
    [Export] private ItemComposite _itemComposite;
    [Export] private ItemInfoPan _itemInfoPan;

    [Export] private ItemSelectRes _itemSelectRes;

    public override void _Ready()
    {
        SignalCenter.OnIsShowInfo += (isShow, res) => _itemInfoPan.DisplayItemInfo(isShow, res);
        SignalCenter.OnOutputRes += (res) => _itemSelectRes.AddItemRes(res);

        _backpackInventory.Init();
        _itemComposite.Init();
        _itemInfoPan.Init();
        _itemSelectRes.Init();

        for (var i = 0; i < _count; i++)
        {
            foreach (var item in _pickedRes)
            {
                _backpackInventory.AddItem(item);
            }
        }
    }
}