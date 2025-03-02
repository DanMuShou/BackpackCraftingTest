using Godot;

public partial class BackpackSignalCenter : Node
{
    [Signal]
    public delegate void OnIsShowInfoEventHandler(bool isShow, BackpackItemRes itemRes);


    public void EmitIsShowInfo(bool isShow, BackpackItemRes itemRes)
    {
        EmitSignalOnIsShowInfo(isShow, itemRes);
    }
}