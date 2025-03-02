using Godot;

public partial class BackpackPanel : Control
{
    [Export] private int _count = 4;
    [Export] private BackpackItemRes[] _backpackItemRes;

    [Export] public BackpackSignalCenter SignalCenter { get; private set; }
    [Export] public ItemSelectCon ItemSelectCon { get; private set; }

    [Export] private BackpackInventory _backpackInventory;
    [Export] private ItemComposite _itemComposite;
    [Export] private ItemCompositeEdit _itemCompositeEdit;
    [Export] private ItemInfoPan _itemInfoPan;

    public void Init()
    {
        SignalCenter.OnIsShowInfo += (isShow, res) => _itemInfoPan.DisplayItemInfo(isShow, res);

        _backpackInventory.Init();
        _itemComposite.Init();
        _itemCompositeEdit.Init();
        _itemInfoPan.Init();
        ItemSelectCon.Init();

        if (_backpackItemRes == null) return;

        for (int i = 0; i < _count; i++)
        {
            _backpackInventory.AddItems(_backpackItemRes);
        }
    }
}