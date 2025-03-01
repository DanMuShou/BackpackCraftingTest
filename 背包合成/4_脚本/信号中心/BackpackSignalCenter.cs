using Godot;

public partial class BackpackSignalCenter : Node
{
    [Signal]
    public delegate void OnIsShowInfoEventHandler(bool isShow, PickedRes itemRes);


    public void EmitIsShowInfo(bool isShow, PickedRes itemRes)
    {
        EmitSignalOnIsShowInfo(isShow, itemRes);
    }
}