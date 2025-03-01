using Godot;

public partial class BackpackPanel : Control
{
    [Export] private int _count = 4;
    [Export] private PickedRes[] _pickedRes;

    [Export] public BackpackSignalCenter SignalCenter { get; private set; }
    [Export] public ItemSelectCon ItemSelectCon { get; private set; }

    [Export] private BackpackInventory _backpackInventory;
    [Export] private ItemComposite _itemComposite;
    [Export] private ItemInfoPan _itemInfoPan;


    public override void _Ready()
    {
        SignalCenter.OnIsShowInfo += (isShow, res) => _itemInfoPan.DisplayItemInfo(isShow, res);

        _backpackInventory.Init();
        _itemComposite.Init();
        _itemInfoPan.Init();
        ItemSelectCon.Init();

        for (int i = 0; i < _count; i++)
        {
            _backpackInventory.AddItems(_pickedRes);
        }
    }
}