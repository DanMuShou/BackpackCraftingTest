using Godot;

public partial class BackpackPanel : Control
{
    [Export] private int _count = 2;
    [Export] private PickedRes[] _pickedRes;

    [Export] private BackpackSignalCenter _signalCenter;
    [Export] private BackpackInventory _backpackInventory;
    [Export] private ItemComposite _itemComposite;
    [Export] private ItemInfoPan _itemInfoPan;

    public override void _Ready()
    {
        _signalCenter.OnIsOutputInfo += (isShow, res) => _itemInfoPan.DisplayItemInfo(isShow, res);

        _backpackInventory.Init(_signalCenter);
        _itemComposite.Init(_signalCenter);
        _itemInfoPan.Init();

        for (var i = 0; i < _count; i++)
        {
            foreach (var item in _pickedRes)
            {
                _backpackInventory.AddItem(item);
            }
        }
    }
}