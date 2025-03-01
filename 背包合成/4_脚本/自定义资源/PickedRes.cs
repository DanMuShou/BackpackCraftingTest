using Godot;

public enum EPickedType
{
    Item,
    Equip,
    Skill,
    Other
}

[GlobalClass, Icon("res://icon.svg")]
public partial class PickedRes : Resource
{
    [Export] public string Name{ get;private set;}
    [Export] public string Description{ get;private set;}
    [Export] public int UniqueId{ get;private set;}
    [Export] public int PickType{ get;private set;}
    [Export] public Texture2D BackpackIcon{ get;private set;}
}