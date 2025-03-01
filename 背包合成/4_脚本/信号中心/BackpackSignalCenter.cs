using Godot;

public partial class BackpackSignalCenter : Node
{
    [Signal]
    public delegate void OnIsShowInfoEventHandler(bool isShow, PickedRes itemRes);

    public void EmitIsShowInfo(bool isShow, PickedRes itemRes) => EmitSignalOnIsShowInfo(isShow, itemRes);

    [Signal]
    public delegate void OnOutputResEventHandler(PickedRes itemRes);

    public void EmitOutputRes(PickedRes itemRes) => EmitSignalOnOutputRes(itemRes);
}