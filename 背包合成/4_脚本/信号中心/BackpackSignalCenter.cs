using Godot;

public partial class BackpackSignalCenter : Node
{
    [Signal]
    public delegate void OnIsOutputInfoEventHandler(bool isShow, PickedRes pickedRes);

    public void EmitIsOutputInfo(bool isShow, PickedRes pickedRes) => EmitSignalOnIsOutputInfo(isShow, pickedRes);
}