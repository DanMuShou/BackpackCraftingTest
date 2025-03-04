using Godot;

public partial class BackpackSignalCenter : Node
{
    [Signal]
    public delegate void OnIsShowInfoEventHandler(bool isShow, BackpackItemRes itemRes);

    public void EmitIsShowInfo(bool isShow, BackpackItemRes itemRes)
        => EmitSignalOnIsShowInfo(isShow, itemRes);

    [Signal]
    public delegate void OnIsSelectGesturePanelEventHandler(bool isSelect, BaseBackpackItemContainerPanel panel);

    public void EmitIsSelectGesturePanel(bool isSelect, BaseBackpackItemContainerPanel panel)
        => EmitSignalOnIsSelectGesturePanel(isSelect, panel);
}